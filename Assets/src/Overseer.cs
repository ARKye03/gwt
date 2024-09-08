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

    public Player player1;
    public Player player2;

    public GameObject player1HandPanel; // Reference to Player 1's hand panel
    public GameObject player2HandPanel; // Reference to Player 2's hand panel
    public GameObject cardPrefab; // Reference to the card prefab

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

        player1 = new GameObject("Player1").AddComponent<Player>();
        player2 = new GameObject("Player2").AddComponent<Player>();

        // Assign hand panels and card prefab to players
        player1.handPanel = player1HandPanel;
        player1.cardPrefab = cardPrefab;
        player2.handPanel = player2HandPanel;
        player2.cardPrefab = cardPrefab;
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

        List<List<Card>> decks = new()
        {
            cardsQuanto.CardsOfIdanai,
            cardsQuanto.CardsOfCelai,
            cardsQuanto.CardsOfYudivain
        };
        System.Random random = new();

        // Assign random decks to players
        int deckIndex1 = random.Next(decks.Count);
        player1.Name = "Player 1";
        allyDeck.cards = new Stack<Card>(decks[deckIndex1]);
        decks.RemoveAt(deckIndex1);

        int deckIndex2 = random.Next(decks.Count);
        player2.Name = "Player 2";
        enemyDeck.cards = new Stack<Card>(decks[deckIndex2]);

        // Initialize player hands
        player1.DrawCards(allyDeck, 10);
        player2.DrawCards(enemyDeck, 10);

        // Shuffle decks
        allyDeck.Shuffle();
        enemyDeck.Shuffle();

        foreach (var item in allyDeck.cards)
        {
            Debug.Log("Ally Card: " + item);
        }

        foreach (var item in enemyDeck.cards)
        {
            Debug.Log("Enemy Card: " + item);
        }
    }
}