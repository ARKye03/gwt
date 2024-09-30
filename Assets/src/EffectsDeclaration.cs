using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class Effects : MonoBehaviour
{
    public static bool SpawnRandomCard(Player player)
    {
        if (player.handPanelManager.cards.Count == 0)
        {
            Debug.LogWarning("Player's hand is empty. Cannot spawn a random card.");
            return false;
        }


        return true;
    }
    private static ushort ReloadHandCounter = 0;
    public static bool ReloadHand(Player player)
    {
        player.handPanelManager.cards.ForEach(card => player.deck.cards.Push(card));
        player.handPanelManager.cards.Clear();
        player.deck.Shuffle();
        player.DrawCards(10);
        ReloadHandCounter++;
        if (ReloadHandCounter == 2)
        {
            return true;
        }
        return false;
    }

    private static ushort DropAndDrawCounter = 0;
    public static bool DropAndDrawCard(Player player)
    {
        List<Card> hand = player.handPanelManager.cards;
        if (hand.Count == 0) return false;

        int randomIndex = Random.Range(0, hand.Count);
        Card droppedCard = hand[randomIndex];
        hand.RemoveAt(randomIndex);

        Card newCard = player.deck.DrawCard;
        hand.Add(newCard);
        player.UpdateHandUI();
        Debug.Log($"Dropped card: {droppedCard.Name} and drew new card: {newCard.Name}");
        DropAndDrawCounter++;
        if (DropAndDrawCounter == 3)
        {
            DropAndDrawCounter = 1;
            return true;
        }
        return false;
    }
    public static bool AllorNothing(Player player)
    {
        // Ensure there are cards in hand
        if (player.handPanelManager.cards.Count == 0)
        {
            return false;
        }

        // Select 3 random cards from the player's hand
        List<Card> selectedCards = new();
        List<Card> remainingCards = new(player.handPanelManager.cards);

        int cardsToSelect = Mathf.Min(3, player.handPanelManager.cards.Count);
        for (int i = 0; i < cardsToSelect; i++)
        {
            int randomIndex = Random.Range(0, remainingCards.Count);
            selectedCards.Add(remainingCards[randomIndex]);
            remainingCards.RemoveAt(randomIndex);
        }

        // Place the selected cards on the board
        foreach (Card card in selectedCards)
        {
            CardManager cardManager = player.handPanelManager.handPanel.transform
                .GetComponentsInChildren<CardManager>()
                .FirstOrDefault(cm => cm.CardData == card);

            if (cardManager != null)
            {
                PlaceCard.PlaceACard(cardManager, player);
            }
        }

        // Discard the remaining cards to the graveyard
        foreach (Card card in remainingCards)
        {
            player.graveyard.Push(card);
        }

        // Update the player's hand
        player.handPanelManager.cards = selectedCards;
        player.UpdateHandUI();

        return true;
    }
}