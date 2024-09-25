using System.Collections.Generic;

public static class CleanManager
{
    public static void CleanField(Board board)
    {
        Player allyPlayer = board.allyPlayer;
        Player enemyPlayer = board.enemyPlayer;
        Stack<Card> allyGraveyard = board.allyGraveyard;
        Stack<Card> enemyGraveyard = board.enemyGraveyard;
        CardSlot climateSlot = board.climateSlot;

        // Clean ally rows
        CleanRow(allyPlayer.MeleeSlots, allyGraveyard);
        CleanRow(allyPlayer.RangedSlots, allyGraveyard);
        CleanRow(allyPlayer.SiegeSlots, allyGraveyard);
        allyPlayer.MeleeBonusSlot.RemoveCard();
        allyPlayer.RangedBonusSlot.RemoveCard();
        allyPlayer.SiegeBonusSlot.RemoveCard();
        climateSlot.RemoveCard();

        // Clean enemy rows
        CleanRow(enemyPlayer.MeleeSlots, enemyGraveyard);
        CleanRow(enemyPlayer.RangedSlots, enemyGraveyard);
        CleanRow(enemyPlayer.SiegeSlots, enemyGraveyard);
        enemyPlayer.MeleeBonusSlot.RemoveCard();
        enemyPlayer.RangedBonusSlot.RemoveCard();
        enemyPlayer.SiegeBonusSlot.RemoveCard();
    }
    private static void CleanRow(CardSlot[] slots, Stack<Card> graveyard)
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
}
