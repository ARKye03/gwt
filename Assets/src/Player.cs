using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name { get; set; }

    [Header("<----------Core---------->")]
    public HandPanelManager handPanelManager;
    public GameObject cardPrefab;
    public Deck deck;
    public Board board;
    public Stack<Card> graveyard = new();

    [Header("<----------Slots---------->")]
    public CardSlot[] MeleeSlots;
    public CardSlot MeleeBonusSlot;
    public CardSlot[] RangedSlots;
    public CardSlot RangedBonusSlot;
    public CardSlot[] SiegeSlots;
    public CardSlot SiegeBonusSlot;
    public CardSlot LeaderSlot;

    private void Awake()
    {
        handPanelManager.cards = new List<Card>();
    }

    public void DrawCards(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            if (deck.cards.Count > 0 && handPanelManager.cards.Count < 10)
            {
                handPanelManager.cards.Add(deck.cards.Pop());
            }
            else
            {
                break;
            }
        }
        UpdateHandUI();
    }

    public void UpdateHandUI()
    {
        foreach (Transform child in handPanelManager.handPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in handPanelManager.cards)
        {
            GameObject cardObject = Instantiate(cardPrefab, handPanelManager.handPanel.transform);
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
        PlayRound(cardManager, this);
    }

    private void PlayRound(CardManager cardManager, Player player)
    {
        PlaceCard.PlaceACard(cardManager, player);
        board.PassTurn();
    }
}