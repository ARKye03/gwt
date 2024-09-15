using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image ImageOfCard;
    [SerializeField] private TextMeshProUGUI NameOfCard;
    [SerializeField] private TextMeshProUGUI DescriptionOfCard;
    [SerializeField] private Image TypeOfUnit;

    private Card cardData;
    public Card CardData
    {
        get => cardData;
        set
        {
            cardData = value;
            if (cardData != null)
            {
                UpdateCardUI();
            }
        }
    }

    public Player player; // Reference to the player

    public void UpdateCardUI()
    {
        if (cardData != null)
        {
            NameOfCard.text = cardData.Name;
            DescriptionOfCard.text = cardData.Description;
            ImageOfCard.sprite = cardData.CardImage;

            if (cardData is UnitCard unitCard)
            {
                TypeOfUnit.sprite = unitCard.TypeOfUnitImage;
                TypeOfUnit.gameObject.SetActive(true);
            }
            else
            {
                TypeOfUnit.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player != null)
        {
            player.OnCardClicked(this);
        }
    }
}