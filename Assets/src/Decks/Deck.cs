using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deck is a MonoBehaviour that manages the deck of cards.
/// </summary>
public class Deck : MonoBehaviour
{
    /// <summary>
    /// The cards in the deck.
    /// </summary>
    public Stack<Card> cards = new();

    /// <summary>
    /// Shuffles the cards in the deck.
    /// </summary>
    public void Shuffle()
    {
        List<Card> cardList = new(cards);
        cards.Clear();
        for (int i = 0; i < cardList.Count; i++)
        {
            Card temp = cardList[i];
            int randomIndex = Random.Range(i, cardList.Count);
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }
        foreach (Card card in cardList)
        {
            cards.Push(card);
        }
    }
    /// <summary>
    /// Draws a card from the deck.
    /// </summary>
    /// <returns>The card drawn.</returns>
    public Card DrawCard => cards.Count == 0 ? null : cards.Pop();

    /// <summary>
    /// Adds a card to a graveyard.
    /// </summary>
    /// <param name="card">The card to add.</param>
    /// <param name="graveyard">The graveyard to add the card to.</param>
    public void AddToGraveyard(Card card, Stack<Card> graveyard)
    {
        graveyard.Push(card);
    }
}