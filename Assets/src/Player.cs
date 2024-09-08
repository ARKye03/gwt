using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public List<Card> hand = new();

    public GameObject handPanel; // Reference to the hand panel
    public GameObject cardPrefab; // Reference to the card prefab

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
        UpdateHandUI(); // Update the hand UI after drawing cards
    }

    private void UpdateHandUI()
    {
        if (handPanel == null)
        {
            Debug.LogError("Hand Panel is not assigned.");
            return;
        }

        // Clear existing cards in the hand panel
        foreach (Transform child in handPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate card prefabs for each card in the hand
        foreach (Card card in hand)
        {
            GameObject cardObject = Instantiate(cardPrefab, handPanel.transform);
            CardManager cardManager = cardObject.GetComponent<CardManager>();

            if (cardManager != null)
            {
                cardManager.cardData = card;
                cardManager.UpdateCardUI();
            }
            else
            {
                Debug.LogError("CardManager component not found on card prefab.");
            }
        }
    }
}