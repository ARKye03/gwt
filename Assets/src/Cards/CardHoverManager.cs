using TMPro;
using UnityEngine;

/// <summary>
/// Manages the display of card details when a card is hovered over in the game.
/// </summary>
public class CardHoverManager : MonoBehaviour
{
    /// <summary>
    /// The panel that will display the hover card.
    /// </summary>
    public GameObject hoverPanel;

    /// <summary>
    /// The prefab used to instantiate the hover card.
    /// </summary>
    public GameObject cardPrefab;

    /// <summary>
    /// The parent object for the card description.
    /// </summary>
    public GameObject cardDescriptionParent;

    /// <summary>
    /// The UI element that displays the card's description.
    /// </summary>
    public TextMeshProUGUI cardDescription;

    /// <summary>
    /// The currently displayed hover card.
    /// </summary>
    private GameObject currentHoverCard;

    /// <summary>
    /// Singleton instance of the CardHoverManager.
    /// </summary>
    public static CardHoverManager _instance { get; private set; }

    /// <summary>
    /// Initializes the singleton instance of the CardHoverManager.
    /// </summary>
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

    /// <summary>
    /// Displays the hover card with the details from the provided CardManager.
    /// </summary>
    /// <param name="cardManager">The CardManager containing the card data to display.</param>
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

    /// <summary>
    /// Hides the hover card.
    /// </summary>
    public void HideCard()
    {
        if (currentHoverCard != null)
        {
            Destroy(currentHoverCard);
        }

        hoverPanel.SetActive(false);
    }
}