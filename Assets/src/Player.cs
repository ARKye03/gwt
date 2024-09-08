using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public List<Card> hand = new();

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
    }
}