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
        if (player != null)
        {
            // Check if it's the correct player's turn
            if (Board._instance.CheckPlayer(player))
            {
                if (cardData.CanBePlayed)
                {
                    player.OnCardClicked(this);
                }
                else
                {
                    if (cardData.Effect is not null)
                    {
                        _ = cardData.Effect(player);
                    }
                    Board._instance.PassTurn();
                }
            }
            else
            {
                Debug.LogWarning("It's not this player's turn.");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(audioClip);
        transform.localScale *= 1.1f;
        Board._instance.cardHoverManager.ShowCard(this); // Show the enlarged card
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        Board._instance.cardHoverManager.HideCard(); // Hide the enlarged card
    }
}