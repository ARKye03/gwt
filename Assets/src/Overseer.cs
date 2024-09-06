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

    void Start()
    {
        allyDeck.Shuffle();
    }
}