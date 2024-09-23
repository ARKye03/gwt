using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image ImageOfCard;
    [SerializeField] private TextMeshProUGUI NameOfCard;
    [SerializeField] private TextMeshProUGUI PowerOfCard;
    [SerializeField] private TextMeshProUGUI DescriptionOfCard;
    [SerializeField] private Image TypeOfUnit;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


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

    public Player player;

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
                PowerOfCard.text = unitCard.power.ToString();
            }
            else
            {
                TypeOfUnit.gameObject.SetActive(false);
                PowerOfCard.text = string.Empty; // Clear the damage value for non-unit cards
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player != null && cardData.CanBePlayed)
        {
            player.OnCardClicked(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(audioClip);
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}