using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
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

    public Deck allyDeck;
    public Stack<Card> allyGraveyard = new Stack<Card>();

    public Deck enemyDeck;
    public Stack<Card> enemyGraveyard = new Stack<Card>();

    void Start()
    {
        allyDeck.Shuffle();
    }
}