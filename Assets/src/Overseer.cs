using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class Board : MonoBehaviour
{
    public static Board _instance { get; private set; }

    [Header("<----------Core---------->")]
    public Camera mainCamera;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI roundCount;
    public CardSlot climateSlot;
    public RoundManager roundManager;
    public CleanManager cleanManager;

    [Header("<----------AllyPlayer---------->")]
    public Player allyPlayer;
    public TextMeshProUGUI allyPower;
    private int allyPowerValue = 0;

    public TextMeshProUGUI allyWinsText;
    private int allyWins = 0;
    public Stack<Card> allyGraveyard = new();

    [Header("<---------EnemyPlayer--------->")]
    public Player enemyPlayer;
    private int enemyWins = 0;
    public TextMeshProUGUI enemyWinsText;
    public Stack<Card> enemyGraveyard = new();
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
        var cardsQuanto = CardsQuanto.Instance;

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

        List<Stack<Card>> decks = new()
        {
            cardsQuanto.CardsOfIdanai,
            cardsQuanto.CardsOfCelai,
            cardsQuanto.CardsOfYudivain
        };

#if DEBUG
        allyPlayer.Name = "Ally Player";
        allyPlayer.deck.cards = decks[0];
        enemyPlayer.Name = "Enemy Player";
        enemyPlayer.deck.cards = decks[1];
#else
        UnityEngine.Random random = new();
        // Assign random decks to players
        int deckIndex1 = random.Next(decks.Count);
        allyPlayer.Name = "Ally Player";
        allyPlayer.deck.cards = decks[deckIndex1];
        decks.RemoveAt(deckIndex1);

        int deckIndex2 = random.Next(decks.Count);
        enemyPlayer.Name = "Enemy Player";
        enemyPlayer.deck.cards = decks[deckIndex2];
#endif

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
    public void PassTurn()
    {
        // Switch the turn to the other player
        allyPlayerIsPlaying = !allyPlayerIsPlaying;

        // Increase the round if it's the ally player's turn
        if (allyPlayerIsPlaying)
        {
            roundManager.IncreaseRound();
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
        allyPowerValue = CalculateTotalPower(allyPlayer.MeleeSlots) + CalculateTotalPower(allyPlayer.RangedSlots) + CalculateTotalPower(allyPlayer.SiegeSlots);
        enemyPowerValue = CalculateTotalPower(enemyPlayer.MeleeSlots) + CalculateTotalPower(enemyPlayer.RangedSlots) + CalculateTotalPower(enemyPlayer.SiegeSlots);

        allyPower.text = $"{allyPowerValue}";
        enemyPower.text = $"{enemyPowerValue}";

        allyPlayer.DrawCards(2);
        enemyPlayer.DrawCards(2);

        if (roundManager.Round == 3)
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
            cleanManager.CleanField(this);

            roundManager.ResetRound();
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
        roundCount.transform.rotation = startRotation;
        winnerText.transform.rotation = startRotation;
        allyWinsText.transform.rotation = startRotation;
        enemyWinsText.transform.rotation = startRotation;
        allyPower.transform.rotation = startRotation;
        enemyPower.transform.rotation = startRotation;
    }
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