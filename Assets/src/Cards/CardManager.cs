using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Card cardData;
    public Image img;
    public TextMeshProUGUI NameofCard;
    public Image TypeofUnit;
    public TextMeshProUGUI Description;

    void Start()
    {
        if (cardData != null)
        {
            UpdateCardUI();
        }
    }

    public void UpdateCardUI()
    {
        NameofCard.text = cardData.Name;
        Description.text = cardData.Description;
        img.sprite = cardData.CardImage;
    }
}