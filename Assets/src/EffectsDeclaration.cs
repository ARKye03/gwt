using UnityEngine;
using System.Collections.Generic;

public class Effects : MonoBehaviour
{
    public static bool ActivateRandomCard(Board board)
    {
        List<Card> hand = board.allyPlayer.hand;
        if (hand.Count == 0) return false;

        int randomIndex = Random.Range(0, hand.Count);
        Card randomCard = hand[randomIndex];
        randomCard.CanBePlayed = true;
        Debug.Log($"Activated random card: {randomCard.Name}");
        return true;
    }

    public static bool DropAndDrawCard(Player player)
    {
        List<Card> hand = player.hand;
        if (hand.Count == 0) return false;

        int randomIndex = Random.Range(0, hand.Count);
        Card droppedCard = hand[randomIndex];
        hand.RemoveAt(randomIndex);

        Card newCard = player.deck.DrawCard;
        hand.Add(newCard);
        Debug.Log($"Dropped card: {droppedCard.Name} and drew new card: {newCard.Name}");
        return true;
    }
}