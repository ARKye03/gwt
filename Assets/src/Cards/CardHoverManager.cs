using TMPro;
using UnityEngine;

public class CardHoverManager : MonoBehaviour
{
    public GameObject hoverPanel;
    public GameObject cardPrefab;
    public GameObject cardDescriptionParent;
    public TextMeshProUGUI cardDescription;

    private GameObject currentHoverCard;
    public static CardHoverManager _instance { get; private set; }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void ShowCard(CardManager cardManager)
    {
        if (currentHoverCard != null)
        {
            Destroy(currentHoverCard);
        }

        // Instantiate a copy of the card and set it as a child of the hover panel
        currentHoverCard = Instantiate(cardPrefab, hoverPanel.transform);
        CardManager hoverCardManager = currentHoverCard.GetComponent<CardManager>();
        cardDescription.text = cardManager.CardData.Description;

        // Copy the card data to the new card
        hoverCardManager.CardData = cardManager.CardData;

        // Adjust RectTransform to take the full height and width of the hover panel
        RectTransform hoverCardRect = currentHoverCard.GetComponent<RectTransform>();
        hoverCardRect.anchorMin = Vector2.zero;
        hoverCardRect.anchorMax = Vector2.one;
        hoverCardRect.offsetMin = Vector2.zero;
        hoverCardRect.offsetMax = Vector2.zero;
        hoverCardRect.pivot = new Vector2(0.5f, 0.5f);

        cardDescriptionParent.transform.SetAsLastSibling();

        // Set the hover panel active
        hoverPanel.SetActive(true);
    }

    public void HideCard()
    {
        if (currentHoverCard != null)
        {
            Destroy(currentHoverCard);
        }

        hoverPanel.SetActive(false);
    }
}