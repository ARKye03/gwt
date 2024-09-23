using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The Tooltip class is responsible for displaying and hiding tooltips when the pointer enters or exits the UI element.
/// /// </summary>
public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// The message to be displayed in the tooltip.
    /// </summary>
    public string msg;
    /// /// 
    /// <summary>
    /// Called when the pointer enters the UI element.
    /// Displays the tooltip with the specified message.
    /// </summary>
    /// <param name="eventData">Current event data.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager._instance.ShowTooltip(msg);
    }

    /// <summary>
    /// Called when the pointer exits the UI element.
    /// Hides the tooltip.
    /// </summary>
    /// <param name="eventData">Current event data.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager._instance.HideTooltip();
    }
}
