using System.Linq;
using UnityEngine;

public static class PlaceCard
{
    public static bool PlaceACard(CardManager cardManager, Player player)
    {
        Card card = cardManager.CardData;
        Debug.Log($"Attempting to place card: {card.Name}");

        bool cardPlaced = false;

        switch (card)
        {
            case UnitCard uc:
                cardPlaced = PlaceUnitCard(cardManager, uc, player);
                break;
            case ClimateCard climateCard:
                cardPlaced = PlaceClimateCard(cardManager, climateCard, player.board.climateSlot);
                break;
            case BaitCard bc:
                Player opponent = player == player.board.allyPlayer ? player.board.enemyPlayer : player.board.allyPlayer;
                cardPlaced = PlaceBaitCard(cardManager, bc, opponent);
                break;
            case BonusCard bonusCard:
                cardPlaced = PlaceBonusCard(cardManager, bonusCard, player);
                break;
            default:
                Debug.LogWarning("Unknown card type.");
                break;
        }

        if (cardPlaced)
        {
            player.handPanelManager.cards.Remove(card);
            player.UpdateHandUI();
        }

        return cardPlaced;
    }

    public static bool PlaceUnitCard(CardManager cardManager, UnitCard uc, Player player)
    {
        CardSlot cardSlot = GetAvailableSlot(uc.TypeofUnit, player);

        if (cardSlot != null)
        {
            PlaceCardInSlot(cardManager, uc, cardSlot);
            Debug.Log($"Placed {uc.TypeofUnit} card: {uc.Name} in slot: {cardSlot.name}");
            return true;
        }
        else
        {
            Debug.LogWarning($"No available {uc.TypeofUnit} slot found.");
            return false;
        }
    }

    public static bool PlaceClimateCard(CardManager cardManager, ClimateCard card, CardSlot climateSlot)
    {
        climateSlot.RemoveCard();
        PlaceCardInSlot(cardManager, card, climateSlot);
        card.ApplyEffect();
        Debug.Log($"Placed climate card: {card.Name} in climate slot.");
        return true;
    }

    public static bool PlaceBaitCard(CardManager cardManager, BaitCard bc, Player opponent)
    {
        CardSlot cardSlot = GetRandomOccupiedSlot(bc.typeofUnit, opponent);

        if (cardSlot != null && cardSlot.CurrentCard is UnitCard)
        {
            // Retrieve the card from the CardSlot
            Card cardToReturn = cardSlot.CurrentCard;

            if (cardToReturn != null)
            {
                // Add the card back to the player's hand
                cardToReturn.CanBePlayed = true;
                opponent.handPanelManager.cards.Add(cardToReturn);
                opponent.UpdateHandUI();
            }

            // Remove the card from the CardSlot
            cardSlot.RemoveCard();

            // Place the bait card in the slot
            PlaceCardInSlot(cardManager, bc, cardSlot);
            Debug.Log($"Placed bait card: {bc.Name} in opponent's {bc.typeofUnit} slot: {cardSlot.name}");
            return true;
        }
        else
        {
            Debug.LogWarning($"No available {bc.typeofUnit} slot found on opponent's side.");
            return false;
        }
    }

    public static bool PlaceBonusCard(CardManager cardManager, BonusCard bonusCard, Player player)
    {
        CardSlot bonusSlot = GetBonusSlot(bonusCard.AffectedRow, player);
        if (bonusSlot != null && bonusSlot.IsOccupied)
        {
            Debug.LogWarning("Bonus slot is already occupied.");
            return false;
        }
        CardSlot[] targetSlots = GetTargetSlots(bonusCard.AffectedRow, player);

        if (targetSlots != null && bonusSlot != null)
        {
            PlaceCardInSlot(cardManager, bonusCard, bonusSlot);
            bonusCard.ApplyEffect(targetSlots);
            return true;
        }
        else
        {
            Debug.LogWarning("Invalid row type or player slots not found.");
            return false;
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

    private static CardSlot GetRandomOccupiedSlot(TypeofUnit unitType, Player player)
    {
        CardSlot[] occupiedSlots = unitType switch
        {
            TypeofUnit.Melee => player.MeleeSlots.Where(s => s.IsOccupied).ToArray(),
            TypeofUnit.Ranged => player.RangedSlots.Where(s => s.IsOccupied).ToArray(),
            TypeofUnit.Siege => player.SiegeSlots.Where(s => s.IsOccupied).ToArray(),
            _ => null,
        };

        if (occupiedSlots == null || occupiedSlots.Length == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, occupiedSlots.Length);
        return occupiedSlots[randomIndex];
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