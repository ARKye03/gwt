using TMPro;
using UnityEngine;

/// <summary>
/// Manages the display of tooltips in the game.
/// </summary>
public class TooltipManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the TooltipManager.
    /// </summary>
    public static TooltipManager _instance;

    /// <summary>
    /// Reference to the TextMeshProUGUI component used to display the tooltip text.
    /// </summary>
    public TextMeshProUGUI textUI;

    /// <summary>
    /// Ensures that only one instance of TooltipManager exists.
    /// </summary>
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    /// <summary>
    /// Hides the tooltip on start.
    /// </summary>
    void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates the position of the tooltip to follow the mouse cursor.
    /// </summary>
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// Displays the tooltip with the specified message.
    /// </summary>
    /// <param name="msg">The message to display in the tooltip.</param>
    public void ShowTooltip(string msg)
    {
        gameObject.SetActive(true);
        textUI.text = msg;
    }

    /// <summary>
    /// Hides the tooltip and clears the text.
    /// </summary>
    public void HideTooltip()
    {
        gameObject.SetActive(false);
        textUI.text = string.Empty;
    }
}
