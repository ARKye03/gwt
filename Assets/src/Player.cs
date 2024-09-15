using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public List<Card> hand = new();

    public GameObject handPanel;
    public GameObject cardPrefab;
    public Board board;

    public void DrawCards(Deck deck, int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            if (deck.cards.Count > 0)
            {
                hand.Add(deck.cards.Pop());
            }
            else
            {
                break;
            }
        }
        UpdateHandUI();
    }

    private void UpdateHandUI()
    {
        foreach (Transform child in handPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in hand)
        {
            GameObject cardObject = Instantiate(cardPrefab, handPanel.transform);
            CardManager cardManager = cardObject.GetComponent<CardManager>();

            if (cardManager != null)
            {
                cardManager.CardData = card;
                cardManager.player = this;
            }
            else
            {
                Debug.LogError("CardManager component not found on card prefab.");
            }

            cardObject.transform.localScale = Vector3.one;
        }
    }

    public void OnCardClicked(CardManager cardManager)
    {
        if (board.allyPlayerIsPlaying && this == board.allyPlayer)
        {
            PlaceCard(cardManager, board.allyMeleeSlots, board.allyRangedSlots, board.allySiegeSlots);
        }
        else if (!board.allyPlayerIsPlaying && this == board.enemyPlayer)
        {
            PlaceCard(cardManager, board.enemyMeleeSlots, board.enemyRangedSlots, board.enemySiegeSlots);
        }
    }

    private void PlaceCard(CardManager cardManager, CardSlot[] meleeSlots, CardSlot[] rangedSlots, CardSlot[] siegeSlots)
    {
        Card card = cardManager.CardData;
        Debug.Log($"Attempting to place card: {card.Name}");

        switch (card)
        {
            case UnitCard uc:
                PlaceUnitCard(cardManager, uc, meleeSlots, rangedSlots, siegeSlots);
                break;
            case ClimateCard:
                PlaceClimateCard(cardManager, card);
                break;
            case BaitCard bc:
                PlaceBaitCard(cardManager, bc);
                break;
            case BonusCard bonusCard:
                PlaceBonusCard(cardManager, bonusCard, meleeSlots, rangedSlots, siegeSlots);
                break;
            default:
                Debug.LogWarning("Unknown card type.");
                break;
        }

        hand.Remove(card);
        UpdateHandUI();

        board.allyPlayerIsPlaying = !board.allyPlayerIsPlaying;
        board.mainCamera.transform.Rotate(0, 0, board.mainCamera.transform.rotation.z == 0 ? 180 : -180);
    }

    private void PlaceBonusCard(CardManager cardManager, BonusCard bonusCard, CardSlot[] meleeSlots, CardSlot[] rangedSlots, CardSlot[] siegeSlots)
    {
        CardSlot[] targetSlots = null;

        switch (bonusCard.AffectedRow)
        {
            case RowType.Melee:
                targetSlots = meleeSlots;
                break;
            case RowType.Ranged:
                targetSlots = rangedSlots;
                break;
            case RowType.Siege:
                targetSlots = siegeSlots;
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
                    bonusCard.ApplyEffect(board);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning($"No available slot found in {bonusCard.AffectedRow} row.");
        }
    }



    private void PlaceUnitCard(CardManager cardManager, UnitCard uc, CardSlot[] meleeSlots, CardSlot[] rangedSlots, CardSlot[] siegeSlots)
    {
        CardSlot cardSlot = null;

        if (uc.typeofUnit == TypeofUnit.Melee)
        {
            cardSlot = meleeSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }
        else if (uc.typeofUnit == TypeofUnit.Ranged)
        {
            cardSlot = rangedSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }
        else if (uc.typeofUnit == TypeofUnit.Siege)
        {
            cardSlot = siegeSlots.FirstOrDefault(slot => !slot.IsOccupied);
        }

        if (cardSlot != null)
        {
            cardManager.transform.SetParent(cardSlot.transform);
            cardManager.transform.localPosition = Vector3.zero;
            cardSlot.PlaceCard(uc, cardManager.gameObject);
            Debug.Log($"Placed {uc.typeofUnit} card: {uc.Name} in slot: {cardSlot.name}");
        }
        else
        {
            Debug.LogWarning($"No available {uc.typeofUnit} slot found.");
        }
    }

    private void PlaceClimateCard(CardManager cardManager, Card card)
    {
        board.climateSlot.RemoveCard();
        cardManager.transform.SetParent(board.climateSlot.transform);
        cardManager.transform.localPosition = Vector3.zero;
        board.climateSlot.PlaceCard(card, cardManager.gameObject);
        Debug.Log($"Placed climate card: {card.Name} in climate slot.");
    }

    private void PlaceBaitCard(CardManager cardManager, BaitCard bc)
    {
        Player opponent = this == board.allyPlayer ? board.enemyPlayer : board.allyPlayer;
        CardSlot[] meleeSlots = opponent == board.allyPlayer ? board.allyMeleeSlots : board.enemyMeleeSlots;
        CardSlot[] rangedSlots = opponent == board.allyPlayer ? board.allyRangedSlots : board.enemyRangedSlots;
        CardSlot[] siegeSlots = opponent == board.allyPlayer ? board.allySiegeSlots : board.enemySiegeSlots;

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
            ApplyBaitCardEffects(bc);
        }
        else
        {
            Debug.LogWarning($"No available {bc.typeofUnit} slot found on opponent's side.");
        }
    }

    private void ApplyBaitCardEffects(BaitCard bc)
    {
        //TODO
        Debug.Log($"Applying effects of bait card: {bc.Name}");
    }
}