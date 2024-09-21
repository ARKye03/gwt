using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Board : MonoBehaviour
{
    public Camera mainCamera;

    public TextMeshProUGUI roundCount;
    private int round = 0;

    public TextMeshProUGUI allyPower;
    private int allyPowerValue = 0;
    public TextMeshProUGUI enemyPower;
    private int enemyPowerValue = 0;

    private int allyWins = 0;
    private int enemyWins = 0;

    public TextMeshProUGUI allyWinsText;
    public TextMeshProUGUI enemyWinsText;

    public Deck allyDeck;

    public CardSlot climateSlot;

    public Stack<Card> allyGraveyard = new();

    public CardSlot[] allyMeleeSlots;

    public CardSlot allyMeleeBonusSlot;

    public CardSlot[] allyRangedSlots;

    public CardSlot allyRangedBonusSlot;

    public CardSlot[] allySiegeSlots;

    public CardSlot allySiegeBonusSlot;
    public CardSlot allyLeaderSlot;

    public CardSlot[] enemyMeleeSlots;

    public CardSlot enemyMeleeBonusSlot;

    public CardSlot[] enemyRangedSlots;

    public CardSlot enemyRangedBonusSlot;

    public CardSlot[] enemySiegeSlots;

    public CardSlot enemySiegeBonusSlot;

    public CardSlot enemyLeaderSlot;
    public Deck enemyDeck;

    public Stack<Card> enemyGraveyard = new();

    public Player allyPlayer;

    public Player enemyPlayer;
    public GameObject player1HandPanel;

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

        allyPower.text = $"Power: {allyPowerValue}";
        enemyPower.text = $"Power: {enemyPowerValue}";

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
            }
            else if (enemyWins == 2)
            {
                Debug.Log("Enemy Player wins the series!");
            }

            // Clean the field after determining the winner
            CleanField();

            ResetRound();
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
        allyWinsText.text = $"W: {allyWins}";
        enemyWinsText.text = $"W: {enemyWins}";
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
    #region Utils // Maybe a partial class takes all of this outs, later
    private void UpdateCount() => roundCount.text = $"Rounds: {Round}";
    public void IncreaseRound()
    {
        Round++;
        UpdateCount();
    }
    public void ResetRound()
    {
        Round = 0;
        UpdateCount();
    }
    public IEnumerator RotateCameraSmoothly(float duration)
    {
        yield return new WaitForSeconds(0.2f);
        Quaternion startRotation = mainCamera.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);
        float elapsedTime = 0;

        float startSize = mainCamera.orthographicSize;
        float zoomInSize = startSize - 0.1f; // Zoom in
        float zoomOutSize = startSize; // Zoom out

        Quaternion startTextRotation = roundCount.transform.rotation;
        Quaternion endTextRotation = startTextRotation * Quaternion.Euler(0, 0, 180);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t); // Ease-in-out

            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            roundCount.transform.rotation = Quaternion.Lerp(startTextRotation, endTextRotation, t);

            if (t < 0.5f)
            {
                mainCamera.orthographicSize = Mathf.Lerp(startSize, zoomInSize, t * 2);
            }
            else
            {
                mainCamera.orthographicSize = Mathf.Lerp(zoomInSize, zoomOutSize, (t - 0.5f) * 2);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.rotation = endRotation;
        mainCamera.orthographicSize = zoomOutSize;
        roundCount.transform.rotation = endTextRotation;
    }
    public void UpdateHandPanelVisibility()
    {
        CanvasGroup allyHandPanelCanvasGroup = allyPlayer.handPanel.GetComponent<CanvasGroup>();
        CanvasGroup enemyHandPanelCanvasGroup = enemyPlayer.handPanel.GetComponent<CanvasGroup>();

        if (allyPlayerIsPlaying)
        {
            allyHandPanelCanvasGroup.alpha = 1;
            allyHandPanelCanvasGroup.interactable = true;
            allyHandPanelCanvasGroup.blocksRaycasts = true;

            enemyHandPanelCanvasGroup.alpha = 0;
            enemyHandPanelCanvasGroup.interactable = false;
            enemyHandPanelCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            allyHandPanelCanvasGroup.alpha = 0;
            allyHandPanelCanvasGroup.interactable = false;
            allyHandPanelCanvasGroup.blocksRaycasts = false;

            enemyHandPanelCanvasGroup.alpha = 1;
            enemyHandPanelCanvasGroup.interactable = true;
            enemyHandPanelCanvasGroup.blocksRaycasts = true;
        }
    }
    #endregion
}