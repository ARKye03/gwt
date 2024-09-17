using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Board : MonoBehaviour
{
    public Camera mainCamera;
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

    /*<---RANDOM SEPARATOR MENTIONED 🔥 WTF IS A KILOMETER 🦅🦅🦅🔊🔊🔊--->*/

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

    public bool allyPlayerIsPlaying = true; // Variable to manage turn logic

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
    public IEnumerator RotateCameraSmoothly(float duration)
    {
        Quaternion startRotation = mainCamera.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.rotation = endRotation;
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
}