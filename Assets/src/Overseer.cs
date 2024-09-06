using UnityEngine;

public class Board : MonoBehaviour
{
    public CardSlot[] allyMeleeSlots;
    public CardSlot[] allyRangedSlots;
    public CardSlot[] allySiegeSlots;

    public CardSlot allyLeaderSlot;

    public CardSlot[] enemyMeleeSlots;
    public CardSlot[] enemyRangedSlots;
    public CardSlot[] enemySiegeSlots;
    public CardSlot enemyLeaderSlot;

    void Start()
    {
    }
}