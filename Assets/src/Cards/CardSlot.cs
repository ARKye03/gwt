using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool IsOccupied { get; private set; }
    public Card CurrentCard { get; private set; }
    public TypeofCard AllowedCardType;
    private GameObject currentCardObject;

    public void PlaceCard(Card card, GameObject cardObject)
    {
        if (!IsOccupied && (card.TypeofCard == AllowedCardType || card is BaitCard))
        {
            CurrentCard = card;
            currentCardObject = cardObject;
            IsOccupied = true;
            card.CanBePlayed = false;
            cardObject.transform.position = transform.position;
            cardObject.transform.localScale = Vector3.one; // Reset the scale
            Debug.Log($"Card {card.Name} placed in slot {name}.");
        }
        else
        {
            Debug.LogWarning($"Card {card.Name} type not allowed in this slot or slot is occupied.");
        }
    }

    public void RemoveCard()
    {
        if (IsOccupied)
        {
            Destroy(currentCardObject); // Destroy the card's game object
            CurrentCard = null;
            currentCardObject = null; // Clear the reference to the card's game object
            IsOccupied = false;
        }
    }
}