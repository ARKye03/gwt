using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    public static bool ReloadHand(Player player)
    {
        player.handPanelManager.cards.ForEach(card => player.deck.cards.Push(card));
        player.handPanelManager.cards.Clear();
        player.deck.Shuffle();
        player.DrawCards(10);
        return true;
    }

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
        return true;
    }
}