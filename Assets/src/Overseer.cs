using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using System.Linq;

public class Board : MonoBehaviour
{
    public static Board _instance { get; private set; }

    [Header("<----------Core---------->")]
    public Camera mainCamera;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI roundCount;
    public CardSlot climateSlot;
    public RoundManager roundManager;
    public GameObject pauseMenu;
    public CardHoverManager cardHoverManager;
    public List<Stack<Card>> decks;

    [Header("<----------AllyPlayer---------->")]
    public Player allyPlayer;
    public TextMeshProUGUI allyPower;
    private int allyPowerValue = 0;

    public TextMeshProUGUI allyWinsText;
    private int allyWins = 0;

    [Header("<---------EnemyPlayer--------->")]
    public Player enemyPlayer;
    private int enemyWins = 0;
    public TextMeshProUGUI enemyWinsText;
    public TextMeshProUGUI enemyPower;
    private int enemyPowerValue = 0;

    [Header("<----------Others---------->")]
    public GameObject cardPrefab;

    public bool allyPlayerIsPlaying = true;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        var cardsQuanto = CardsQuanto._instance;
        ValidateCardsQuanto(cardsQuanto);

        decks = InitDecks(cardsQuanto);

        LoadPlayerDecks();

        // Place leader cards in the respective slots
        PlaceLeaderCard(allyPlayer, allyPlayer.deck, allyPlayer.LeaderSlot);
        PlaceLeaderCard(enemyPlayer, enemyPlayer.deck, enemyPlayer.LeaderSlot);

        // Shuffle decks
        allyPlayer.deck.Shuffle();
        enemyPlayer.deck.Shuffle();

        // Initialize player hands
        allyPlayer.DrawCards(10);
        enemyPlayer.DrawCards(10);

        // Update hand panel visibility at the start of the game
        UpdateHandPanelVisibility();
    }

    private void LoadPlayerDecks()
    {
        allyPlayer.Name = "Ally Player";
        enemyPlayer.Name = "Enemy Player";
#if DEBUG
        allyPlayer.deck.cards = decks[0];
        enemyPlayer.deck.cards = decks[1];
#else
        // Assign selected decks to players
        allyPlayer.deck.cards = decks[GameData.AllyPlayerDeckIndex];
        enemyPlayer.deck.cards = decks[GameData.EnemyPlayerDeckIndex];
#endif
    }

    private static void ValidateCardsQuanto(CardsQuanto cardsQuanto)
    {
        if (cardsQuanto == null)
        {
            Debug.LogError("CardsQuanto instance is null");
            return;
        }

        if (cardsQuanto.CardsOfIdanai == null || cardsQuanto.CardsOfYudivain == null || cardsQuanto.CardsOfCelai == null)
        {
            Debug.LogError("CardsQuanto card lists are not initialized");
            return;
        }
    }

    public List<Stack<Card>> InitDecks(CardsQuanto cardsQuanto)
    {
        return new()
        {
            cardsQuanto.CardsOfIdanai,
            cardsQuanto.CardsOfCelai,
            cardsQuanto.CardsOfYudivain
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
    }

    public void PassTurn()
    {
        // Switch the turn to the other player
        allyPlayerIsPlaying = !allyPlayerIsPlaying;

        // Increase the round if it's the ally player's turn
        if (allyPlayerIsPlaying)
        {
            allyPlayer.DrawCards(2);
            roundManager.IncreaseRound();
        }
        else
        {
            enemyPlayer.DrawCards(2);
        }

        // Rotate the camera to the other player
        StartCoroutine(RotateElements(1.0f));

        // Update hand panel visibility after passing the turn
        UpdateHandPanelVisibility();

        // Calculate and display power at the end of each round
        CalculateAndDisplayPower();
    }

    private void PlaceLeaderCard(Player player, Deck deck, CardSlot leaderSlot)
    {
        if (deck.cards.Count > 0 && deck.cards.Peek() is LeaderCard leaderCard)
        {
            // Pop the leader card from the deck
            deck.cards.Pop();

            // Instantiate the card prefab and place it in the leader slot
            GameObject leaderCardObject = Instantiate(cardPrefab, leaderSlot.transform);
            CardManager cardManager = leaderCardObject.GetComponent<CardManager>();
            cardManager.player = player;

            if (cardManager != null)
            {
                cardManager.CardData = leaderCard;
                leaderSlot.PlaceCard(leaderCard, leaderCardObject);
            }
            else
            {
                Debug.LogError("CardManager component not found on card prefab.");
            }
        }
        else
        {
            Debug.LogError($"{player.Name} does not have a leader card in the deck.");
        }
    }

    public void CalculateAndDisplayPower()
    {
        UpdatePowerInField();
        allyPowerValue = CalculateTotalPower(allyPlayer.MeleeSlots) + CalculateTotalPower(allyPlayer.RangedSlots) + CalculateTotalPower(allyPlayer.SiegeSlots);
        enemyPowerValue = CalculateTotalPower(enemyPlayer.MeleeSlots) + CalculateTotalPower(enemyPlayer.RangedSlots) + CalculateTotalPower(enemyPlayer.SiegeSlots);

        allyPower.text = $"{allyPowerValue}";
        enemyPower.text = $"{enemyPowerValue}";

        if (roundManager.Round == 5)
        {
            if (allyPowerValue > enemyPowerValue)
            {
                allyWins++;
                Debug.Log("Ally Player won the battle!");
            }
            else if (enemyPowerValue > allyPowerValue)
            {
                enemyWins++;
                Debug.Log("Enemy Player won the battle!");
            }
            else
            {
                Debug.Log("The battle is a draw!");
            }

            UpdateWinsDisplay();

            if (allyWins == 2)
            {
                Debug.Log("Ally Player wins the series!");
                winnerText.text = "Ally player wins!";
            }
            else if (enemyWins == 2)
            {
                Debug.Log("Enemy Player wins the series!");
                winnerText.text = "Enemy player wins!";
            }

            // Clean the field after determining the winner
            CleanManager.CleanField(this);

            roundManager.ResetRound();
        }
    }

    private void UpdatePowerInField()
    {
        // Reset the power of each unit card to its default value
        ResetUnitCardPower(allyPlayer);
        ResetUnitCardPower(enemyPlayer);

        // Apply climate card effect if present
        if (climateSlot.CurrentCard is ClimateCard climateCard)
        {
            _ = climateCard.ApplyEffect();
        }

        // Apply bonus card effects for ally and enemy players
        ApplyBonusCardEffects(allyPlayer);
        ApplyBonusCardEffects(enemyPlayer);
    }

    private void ResetUnitCardPower(Player player)
    {
        ResetPowerInSlots(player.MeleeSlots);
        ResetPowerInSlots(player.RangedSlots);
        ResetPowerInSlots(player.SiegeSlots);
    }

    private void ResetPowerInSlots(CardSlot[] slots)
    {
        foreach (var slot in slots)
        {
            if (slot.IsOccupied && slot.CurrentCard is UnitCard unitCard)
            {
                unitCard.ResetPower();
            }
        }
    }

    private void ApplyBonusCardEffects(Player player)
    {
        ApplyBonusCardEffect(player.MeleeBonusSlot.CurrentCard, player.MeleeSlots);
        ApplyBonusCardEffect(player.RangedBonusSlot.CurrentCard, player.RangedSlots);
        ApplyBonusCardEffect(player.SiegeBonusSlot.CurrentCard, player.SiegeSlots);
    }

    private void ApplyBonusCardEffect(Card currentCard, CardSlot[] targetSlots)
    {
        if (currentCard is BonusCard bonusCard)
        {
            _ = bonusCard.ApplyEffect(targetSlots);
        }
    }

    private void UpdateWinsDisplay()
    {
        allyWinsText.text = $"{allyWins}";
        enemyWinsText.text = $"{enemyWins}";
    }

    private int CalculateTotalPower(CardSlot[] slots)
    {
        int totalPower = 0;
        foreach (var slot in slots)
        {
            if (slot.IsOccupied && slot.CurrentCard is UnitCard unitCard)
            {
                totalPower += unitCard.power;
            }
        }
        return totalPower;
    }

    public IEnumerator RotateElements(float duration)
    {
        yield return new WaitForSeconds(0.2f); // 200ms delay

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);
        float elapsedTime = 0;

        Quaternion startTextRotation = roundCount.transform.rotation;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = EaseInOut(t);

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            RotateUIElements(startTextRotation);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    private void RotateUIElements(Quaternion startRotation)
    {
        cardHoverManager.transform.rotation = startRotation;
        roundCount.transform.rotation = startRotation;
        winnerText.transform.rotation = startRotation;
        allyWinsText.transform.rotation = startRotation;
        enemyWinsText.transform.rotation = startRotation;
        allyPower.transform.rotation = startRotation;
        enemyPower.transform.rotation = startRotation;
    }

    public bool CheckPlayer(Player player)
    => (player == allyPlayer && allyPlayerIsPlaying) ||
                (player == enemyPlayer && !allyPlayerIsPlaying);

    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    public void UpdateHandPanelVisibility()
    {
        if (allyPlayerIsPlaying)
        {
            allyPlayer.handPanelManager.ShowPanel();
            enemyPlayer.handPanelManager.HidePanel();
        }
        else
        {
            allyPlayer.handPanelManager.HidePanel();
            enemyPlayer.handPanelManager.ShowPanel();
        }
    }
}