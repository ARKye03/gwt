using System.Linq;
using UnityEngine;

public static class PlaceCard
{
    public static void PlaceACard(CardManager cardManager, Player player)
    {
        Card card = cardManager.CardData;
        Debug.Log($"Attempting to place card: {card.Name}");

        switch (card)
        {
            case UnitCard uc:
                PlaceUnitCard(cardManager, uc, player);
                break;
            case ClimateCard climateCard:
                PlaceClimateCard(cardManager, climateCard, player.board.climateSlot);
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

    public static void PlaceUnitCard(CardManager cardManager, UnitCard uc, Player player)
    {
        CardSlot cardSlot = GetAvailableSlot(uc.TypeofUnit, player);

        if (cardSlot != null)
        {
            PlaceCardInSlot(cardManager, uc, cardSlot);
            Debug.Log($"Placed {uc.TypeofUnit} card: {uc.Name} in slot: {cardSlot.name}");
        }
        else
        {
            Debug.LogWarning($"No available {uc.TypeofUnit} slot found.");
        }
    }

    public static void PlaceClimateCard(CardManager cardManager, ClimateCard card, CardSlot climateSlot)
    {
        climateSlot.RemoveCard();
        PlaceCardInSlot(cardManager, card, climateSlot);
        card.ApplyEffect(card.AffectedRow);
        Debug.Log($"Placed climate card: {card.Name} in climate slot.");
    }

    public static void PlaceBaitCard(CardManager cardManager, BaitCard bc, Player opponent)
    {
        CardSlot cardSlot = GetAvailableSlot(bc.typeofUnit, opponent);

        if (cardSlot != null)
        {
            PlaceCardInSlot(cardManager, bc, cardSlot);
            Debug.Log($"Placed bait card: {bc.Name} in opponent's {bc.typeofUnit} slot: {cardSlot.name}");
            // ApplyBaitCardEffects(bc); // This should be handled in the Player class or another appropriate place
        }
        else
        {
            Debug.LogWarning($"No available {bc.typeofUnit} slot found on opponent's side.");
        }
    }

    public static void PlaceBonusCard(CardManager cardManager, BonusCard bonusCard, Player player)
    {
        CardSlot[] targetSlots = GetTargetSlots(bonusCard.AffectedRow, player);
        CardSlot bonusSlot = GetBonusSlot(bonusCard.AffectedRow, player);

        if (targetSlots != null && bonusSlot != null)
        {
            PlaceCardInSlot(cardManager, bonusCard, bonusSlot);
            bonusCard.ApplyEffect(targetSlots);
        }
        else
        {
            Debug.LogWarning("Invalid row type or player slots not found.");
        }
    }

    private static CardSlot GetAvailableSlot(TypeofUnit unitType, Player player)
    {
        return unitType switch
        {
            TypeofUnit.Melee => player.MeleeSlots.FirstOrDefault(slot => !slot.IsOccupied),
            TypeofUnit.Ranged => player.RangedSlots.FirstOrDefault(slot => !slot.IsOccupied),
            TypeofUnit.Siege => player.SiegeSlots.FirstOrDefault(slot => !slot.IsOccupied),
            _ => null,
        };
    }

    private static CardSlot[] GetTargetSlots(RowType rowType, Player player)
    {
        return rowType switch
        {
            RowType.Melee => player.MeleeSlots,
            RowType.Ranged => player.RangedSlots,
            RowType.Siege => player.SiegeSlots,
            _ => null,
        };
    }

    private static CardSlot GetBonusSlot(RowType rowType, Player player)
    {
        return rowType switch
        {
            RowType.Melee => player.MeleeBonusSlot,
            RowType.Ranged => player.RangedBonusSlot,
            RowType.Siege => player.SiegeBonusSlot,
            _ => null,
        };
    }

    private static void PlaceCardInSlot(CardManager cardManager, Card card, CardSlot cardSlot)
    {
        cardManager.transform.SetParent(cardSlot.transform);
        cardManager.transform.localPosition = Vector3.zero;
        cardSlot.PlaceCard(card, cardManager.gameObject);
    }
}