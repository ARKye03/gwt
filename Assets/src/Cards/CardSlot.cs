using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool IsOccupied { get; private set; }
    public Card CurrentCard { get; private set; }
    public TypeofCard AllowedCardType;
    private GameObject currentCardObject;

    public void PlaceCard(Card card, GameObject cardObject)
    {
        if (IsOccupied)
        {
            Debug.LogWarning($"Slot {name} is already occupied.");
            return;
        }

        if (card.TypeofCard != AllowedCardType && card is not BaitCard)
        {
            Debug.LogWarning($"Card {card.Name} type not allowed in slot {name}.");
            return;
        }

        CurrentCard = card;
        currentCardObject = cardObject;
        IsOccupied = true;
        card.CanBePlayed = false;
        cardObject.transform.position = transform.position;
        cardObject.transform.localScale = Vector3.one;
        Debug.Log($"Card {card.Name} placed in slot {name}.");
    }

    public void RemoveCard()
    {
        if (!IsOccupied)
        {
            Debug.LogWarning($"Slot {name} is already empty.");
            return;
        }

        Destroy(currentCardObject);
        CurrentCard = null;
        currentCardObject = null;
        IsOccupied = false;
        Debug.Log($"Card removed from slot {name}.");
    }
}