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
        if (cardData == null) return;

        NameOfCard.text = cardData.Name;
        DescriptionOfCard.text = cardData.Description;
        ImageOfCard.sprite = cardData.CardImage;

        switch (cardData)
        {
            case UnitCard unitCard:
                UpdateUnitCardUI(unitCard);
                break;
            case BaitCard baitCard:
                UpdateBaitCardUI(baitCard);
                break;
            case ClimateCard climateCard:
                UpdateClimateCardUI(climateCard);
                break;
            case BonusCard bonusCard:
                UpdateBonusCardUI(bonusCard);
                break;
            default:
                ClearCardUI();
                break;
        }
    }

    private void UpdateUnitCardUI(UnitCard unitCard)
    {
        TypeOfUnit.sprite = unitCard.TypeOfUnitImage;
        TypeOfUnit.gameObject.SetActive(true);
        PowerOfCard.text = unitCard.power.ToString();
    }

    private void UpdateBaitCardUI(BaitCard baitCard)
    {
        TypeOfUnit.sprite = baitCard.TypeOfUnitImage;
        TypeOfUnit.gameObject.SetActive(true);
        PowerOfCard.text = "0"; // Clear the power value for bait cards
        PowerOfCard.color = Color.blue;
    }

    private void UpdateClimateCardUI(ClimateCard climateCard)
    {
        TypeOfUnit.gameObject.SetActive(false);
        PowerOfCard.text = $"{climateCard.ClimatePower}";
        PowerOfCard.color = Color.green;
    }

    private void UpdateBonusCardUI(BonusCard bonusCard)
    {
        TypeOfUnit.gameObject.SetActive(false);
        PowerOfCard.text = $"{bonusCard.initialBoost}";
        PowerOfCard.color = Color.magenta;
    }

    private void ClearCardUI()
    {
        TypeOfUnit.gameObject.SetActive(false);
        PowerOfCard.text = string.Empty; // Clear the power value for other cards
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (player == null) return;

        if (!Board._instance.CheckPlayer(player))
        {
            Debug.LogWarning("It's not this player's turn.");
            return;
        }

        if (cardData.CanBePlayed)
        {
            bool cardPlayed = player.OnCardClicked(this);
            if (cardPlayed)
            {
                Board._instance.PassTurn();
            }
        }
        else
        {
            if (cardData.Effect?.Invoke(player) is true)
            {
                Board._instance.PassTurn();
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