using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool IsOccupied { get; private set; }
    public Card CurrentCard { get; private set; }
    public TypeofCard AllowedCardType;

    public void PlaceCard(Card card, GameObject cardObject)
    {
        if (!IsOccupied && card.TypeofCard == AllowedCardType)
        {
            CurrentCard = card;
            IsOccupied = true;
            cardObject.transform.position = transform.position;
        }
        else
        {
            Debug.LogWarning("Card type not allowed in this slot or slot is occupied.");
        }
    }

    public void RemoveCard()
    {
        if (IsOccupied)
        {
            CurrentCard = null;
            IsOccupied = false;
        }
    }
}