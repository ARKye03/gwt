using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public Deck allyDeck;
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
}