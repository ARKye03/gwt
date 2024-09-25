using System.Linq;
using UnityEngine;

public class PlaceCard
{
    public void PlaceACard(CardManager cardManager, Player player)
    {
        Card card = cardManager.CardData;
        Debug.Log($"Attempting to place card: {card.Name}");

        switch (card)
        {
            case UnitCard uc:
                PlaceUnitCard(cardManager, uc, player);
                break;
            case ClimateCard:
                PlaceClimateCard(cardManager, card, player.board.climateSlot);
                break;
            case BaitCard bc:
                Player opponent = player == player.board.allyPlayer ? player.board.enemyPlayer : player.board.allyPlayer;
                PlaceBaitCard(cardManager, bc, opponent);
                break;
            case BonusCard bonusCard:
                PlaceBonusCard(cardManager, bonusCard, player);
                break;
            default:
                Debug.LogWarning("Unknown card type.");
                break;
        }

        player.handPanelManager.cards.Remove(card);
        player.UpdateHandUI();
    }

    public void PlaceUnitCard(CardManager cardManager, UnitCard uc, Player player)
    {
        CardSlot cardSlot = null;

        if (uc.TypeofUnit == TypeofUnit.Melee)
        {
            cardSlot = player.MeleeSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }
        else if (uc.TypeofUnit == TypeofUnit.Ranged)
        {
            cardSlot = player.RangedSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }
        else if (uc.TypeofUnit == TypeofUnit.Siege)
        {
            cardSlot = player.SiegeSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }

        if (cardSlot != null)
        {
            cardManager.transform.SetParent(cardSlot.transform);
            cardManager.transform.localPosition = Vector3.zero;
            cardSlot.PlaceCard(uc, cardManager.gameObject);
            Debug.Log($"Placed {uc.TypeofUnit} card: {uc.Name} in slot: {cardSlot.name}");
        }
        else
        {
            Debug.LogWarning($"No available {uc.TypeofUnit} slot found.");
        }
    }

    public void PlaceClimateCard(CardManager cardManager, Card card, CardSlot climateSlot)
    {
        climateSlot.RemoveCard();
        cardManager.transform.SetParent(climateSlot.transform);
        cardManager.transform.localPosition = Vector3.zero;
        climateSlot.PlaceCard(card, cardManager.gameObject);
        Debug.Log($"Placed climate card: {card.Name} in climate slot.");
    }

    public void PlaceBaitCard(CardManager cardManager, BaitCard bc, Player opponent)
    {
        CardSlot[] meleeSlots = opponent.MeleeSlots;
        CardSlot[] rangedSlots = opponent.RangedSlots;
        CardSlot[] siegeSlots = opponent.SiegeSlots;

        CardSlot cardSlot = null;

        if (bc.typeofUnit == TypeofUnit.Melee)
        {
            cardSlot = meleeSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }
        else if (bc.typeofUnit == TypeofUnit.Ranged)
        {
            cardSlot = rangedSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }
        else if (bc.typeofUnit == TypeofUnit.Siege)
        {
            cardSlot = siegeSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }

        if (cardSlot != null)
        {
            cardManager.transform.SetParent(cardSlot.transform);
            cardManager.transform.localPosition = Vector3.zero;
            cardSlot.PlaceCard(bc, cardManager.gameObject);
            Debug.Log($"Placed bait card: {bc.Name} in opponent's {bc.typeofUnit} slot: {cardSlot.name}");
            // ApplyBaitCardEffects(bc); // This should be handled in the Player class or another appropriate place
        }
        else
        {
            Debug.LogWarning($"No available {bc.typeofUnit} slot found on opponent's side.");
        }
    }

    public void PlaceBonusCard(CardManager cardManager, BonusCard bonusCard, Player player)
    {
        CardSlot[] targetSlots = null;

        switch (bonusCard.AffectedRow)
        {
            case RowType.Melee:
                targetSlots = player.MeleeSlots;
                break;
            case RowType.Ranged:
                targetSlots = player.RangedSlots;
                break;
            case RowType.Siege:
                targetSlots = player.SiegeSlots;
                break;
        }

        if (targetSlots != null)
        {
            foreach (var slot in targetSlots)
            {
                if (!slot.IsOccupied)
                {
                    cardManager.transform.SetParent(slot.transform);
                    cardManager.transform.localPosition = Vector3.zero;
                    slot.PlaceCard(bonusCard, cardManager.gameObject);
                    Debug.Log($"Placed bonus card: {bonusCard.Name} in {bonusCard.AffectedRow} row.");
                    // bonusCard.ApplyEffect(cardManager.player.board);
                    // TODO
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning($"No available slot found in {bonusCard.AffectedRow} row.");
        }
    }
}