using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class Board : MonoBehaviour
{
    [Header("<----------Core---------->")]
    public Camera mainCamera;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI roundCount;
    public CardSlot climateSlot;
    public RoundManager roundManager;
    private int round = 0;

    [Header("<----------Ally---------->")]
    public Player allyPlayer;
    public GameObject player1HandPanel;

    public TextMeshProUGUI allyPower;
    private int allyPowerValue = 0;

    public TextMeshProUGUI allyWinsText;
    private int allyWins = 0;
    public Deck allyDeck;

    public Stack<Card> allyGraveyard = new();
    public CardSlot[] allyMeleeSlots;
    public CardSlot allyMeleeBonusSlot;
    public CardSlot[] allyRangedSlots;
    public CardSlot allyRangedBonusSlot;
    public CardSlot[] allySiegeSlots;
    public CardSlot allySiegeBonusSlot;
    public CardSlot allyLeaderSlot;

    [Header("<----------Enemy---------->")]
    private int enemyWins = 0;
    public TextMeshProUGUI enemyWinsText;
    public CardSlot[] enemyMeleeSlots;

    public CardSlot enemyMeleeBonusSlot;

    public CardSlot[] enemyRangedSlots;

    public CardSlot enemyRangedBonusSlot;

    public CardSlot[] enemySiegeSlots;

    public CardSlot enemySiegeBonusSlot;

    public CardSlot enemyLeaderSlot;
    public Deck enemyDeck;

    public Stack<Card> enemyGraveyard = new();

    public Player enemyPlayer;
    public TextMeshProUGUI enemyPower;
    private int enemyPowerValue = 0;

    public GameObject player2HandPanel;

    public GameObject cardPrefab;

    public bool allyPlayerIsPlaying = true;

    public int Round { get => round; set => round = value; }

    void Awake()
    {
        if (allyDeck == null)
        {
            allyDeck = new GameObject("AllyDeck").AddComponent<Deck>();
        }

        if (enemyDeck == null)
        {
            enemyDeck = new GameObject("EnemyDeck").AddComponent<Deck>();
        }

        // Assign hand panels and card prefab to players
        allyPlayer.handPanel = player1HandPanel;
        allyPlayer.cardPrefab = cardPrefab;
        enemyPlayer.handPanel = player2HandPanel;
        enemyPlayer.cardPrefab = cardPrefab;

        // Assign board reference to players
        allyPlayer.board = this;
        enemyPlayer.board = this;
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
        System.Random random = new();

        // Assign random decks to players
        int deckIndex1 = random.Next(decks.Count);
        allyPlayer.Name = "Ally Player";
        allyDeck.cards = decks[deckIndex1];
        decks.RemoveAt(deckIndex1);

        int deckIndex2 = random.Next(decks.Count);
        enemyPlayer.Name = "Enemy Player";
        enemyDeck.cards = decks[deckIndex2];

        // Place leader cards in the respective slots
        PlaceLeaderCard(allyPlayer, allyDeck, allyLeaderSlot);
        PlaceLeaderCard(enemyPlayer, enemyDeck, enemyLeaderSlot);

        // Shuffle decks
        allyDeck.Shuffle();
        enemyDeck.Shuffle();

        // Initialize player hands
        allyPlayer.DrawCards(allyDeck, 10);
        enemyPlayer.DrawCards(enemyDeck, 10);

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
        allyPowerValue = CalculateTotalPower(allyMeleeSlots) + CalculateTotalPower(allyRangedSlots) + CalculateTotalPower(allySiegeSlots);
        enemyPowerValue = CalculateTotalPower(enemyMeleeSlots) + CalculateTotalPower(enemyRangedSlots) + CalculateTotalPower(enemySiegeSlots);

        allyPower.text = $"{allyPowerValue}";
        enemyPower.text = $"{enemyPowerValue}";

        allyPlayer.DrawCards(allyDeck, 2);
        enemyPlayer.DrawCards(enemyDeck, 2);

        if (Round == 3)
        {
            if (allyPowerValue > enemyPowerValue)
            {
                allyWins++;
                Debug.Log("Ally Player won the turn!");
            }
            else if (enemyPowerValue > allyPowerValue)
            {
                enemyWins++;
                Debug.Log("Enemy Player won the turn!");
            }
            else
            {
                Debug.Log("The turn is a draw!");
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
            CleanField();

            roundManager.ResetRound();
        }
    }
    public void CleanField()
    {
        // Clean ally rows
        CleanRow(allyMeleeSlots, allyGraveyard);
        CleanRow(allyRangedSlots, allyGraveyard);
        CleanRow(allySiegeSlots, allyGraveyard);
        allyMeleeBonusSlot.RemoveCard();
        allyRangedBonusSlot.RemoveCard();
        allySiegeBonusSlot.RemoveCard();
        climateSlot.RemoveCard();

        // Clean enemy rows
        CleanRow(enemyMeleeSlots, enemyGraveyard);
        CleanRow(enemyRangedSlots, enemyGraveyard);
        CleanRow(enemySiegeSlots, enemyGraveyard);
        enemyMeleeBonusSlot.RemoveCard();
        enemyRangedBonusSlot.RemoveCard();
        enemySiegeBonusSlot.RemoveCard();
    }
    private void CleanRow(CardSlot[] slots, Stack<Card> graveyard)
    {
        foreach (var slot in slots)
        {
            if (slot.IsOccupied)
            {
                graveyard.Push(slot.CurrentCard);
                slot.RemoveCard();
            }
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
        HandPanelManager allyHandPanelManager = allyPlayer.handPanel.GetComponent<HandPanelManager>();
        HandPanelManager enemyHandPanelManager = enemyPlayer.handPanel.GetComponent<HandPanelManager>();

        if (allyPlayerIsPlaying)
        {
            allyHandPanelManager.ShowPanel();
            enemyHandPanelManager.HidePanel();
        }
        else
        {
            allyHandPanelManager.HidePanel();
            enemyHandPanelManager.ShowPanel();
        }
    }
}