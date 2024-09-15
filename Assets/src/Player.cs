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
            PlaceCard(cardManager.CardData);
        }
        else if (!board.allyPlayerIsPlaying && this == board.enemyPlayer)
        {
            PlaceCard(cardManager.CardData);
        }
    }

    private void PlaceCard(Card card)
    {
        Debug.Log($"Attempting to place card: {card.Name}");

        switch (card)
        {
            case UnitCard uc:
                if (uc.typeofUnit is TypeofUnit.Melee)
                {
                    CardSlot cardSlot = board.allyMeleeSlots.FirstOrDefault(slot => !slot.IsOccupied);
                    if (cardSlot != null)
                    {
                        GameObject cardObject = Instantiate(cardPrefab, cardSlot.transform);
                        cardSlot.PlaceCard(uc, cardObject);
                        Debug.Log($"Placed melee card: {uc.Name} in slot: {cardSlot.name}");
                    }
                    else
                    {
                        Debug.LogWarning("No available melee slot found.");
                    }
                }
                else if (uc.typeofUnit is TypeofUnit.Ranged)
                {
                    CardSlot cardSlot = board.allyRangedSlots.FirstOrDefault(slot => !slot.IsOccupied);
                    if (cardSlot != null)
                    {
                        GameObject cardObject = Instantiate(cardPrefab, cardSlot.transform);
                        cardSlot.PlaceCard(uc, cardObject);
                        Debug.Log($"Placed ranged card: {uc.Name} in slot: {cardSlot.name}");
                    }
                    else
                    {
                        Debug.LogWarning("No available ranged slot found.");
                    }
                }
                else if (uc.typeofUnit is TypeofUnit.Siege)
                {
                    CardSlot cardSlot = board.allySiegeSlots.FirstOrDefault(slot => !slot.IsOccupied);
                    if (cardSlot != null)
                    {
                        GameObject cardObject = Instantiate(cardPrefab, cardSlot.transform);
                        cardSlot.PlaceCard(uc, cardObject);
                        Debug.Log($"Placed siege card: {uc.Name} in slot: {cardSlot.name}");
                    }
                    else
                    {
                        Debug.LogWarning("No available siege slot found.");
                    }
                }
                break;
            case ClimateCard:
                board.climateSlot.RemoveCard();
                GameObject climateCardObject = Instantiate(cardPrefab, board.climateSlot.transform);
                board.climateSlot.PlaceCard(card, climateCardObject);
                Debug.Log($"Placed climate card: {card.Name} in climate slot.");
                break;
            case BaitCard:
                // Implement BaitCard logic here
                break;
            case BonusCard:
                // Implement BonusCard logic here
                break;
            default:
                Debug.LogWarning("Unknown card type.");
                break;
        }

        hand.Remove(card);
        UpdateHandUI();

        board.allyPlayerIsPlaying = !board.allyPlayerIsPlaying;
    }
}