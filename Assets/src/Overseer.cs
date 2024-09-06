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

    void Awake()
    {
        if (allyDeck == null)
        {
            allyDeck = new Deck();
        }

        if (enemyDeck == null)
        {
            enemyDeck = new Deck();
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

        if (cardsQuanto.CardsOfIdanai == null || cardsQuanto.CardsOfYudivain == null)
        {
            Debug.LogError("CardsQuanto card lists are not initialized");
            return;
        }

        allyDeck.cards = new Stack<Card>(cardsQuanto.CardsOfIdanai);
        enemyDeck.cards = new Stack<Card>(cardsQuanto.CardsOfYudivain);

        allyDeck.Shuffle();
        foreach (var item in allyDeck.cards)
        {
            Debug.Log("Ally Card: " + item);
        }

        enemyDeck.Shuffle();
        foreach (var item in enemyDeck.cards)
        {
            Debug.Log("Enemy Card: " + item);
        }
    }
}