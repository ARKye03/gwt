using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Stack<Card> cards = new();

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

    public Card DrawCard => cards.Count == 0 ? null : cards.Pop();

    public void AddToGraveyard(Card card, Stack<Card> graveyard)
    {
        graveyard.Push(card);
    }
}