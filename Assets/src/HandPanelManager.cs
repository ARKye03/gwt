using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages the hover effect for the hand panel in the game.
/// </summary>
public class HandPanelManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// The position of the panel when it is hidden.
    /// </summary>
    public Vector3 hiddenPosition;

    /// <summary>
    /// The position of the panel when it is visible.
    /// </summary>
    public Vector3 visiblePosition;

    /// <summary>
    /// The speed at which the panel transitions between hidden and visible positions.
    /// </summary>
    public float transitionSpeed = 5f;

    private Vector3 targetPosition;
    private Vector3 targetScale;

    /// <summary>
    /// Initializes the panel to the hidden position and scale.
    /// </summary>
    void Start()
    {
        targetPosition = hiddenPosition;
        transform.localPosition = hiddenPosition;
        targetScale = Vector3.one;
        transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Updates the panel's position and scale based on the target values.
    /// </summary>
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * transitionSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);
    }

    /// <summary>
    /// Handles the pointer enter event to make the panel visible and larger.
    /// </summary>
    /// <param name="eventData">The event data associated with the pointer enter event.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetPosition = visiblePosition;
        targetScale = new Vector3(1.4f, 1.4f, 1.4f);
    }

    /// <summary>
    /// Handles the pointer exit event to hide the panel and reset its scale.
    /// </summary>
    /// <param name="eventData">The event data associated with the pointer exit event.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        targetPosition = hiddenPosition;
        targetScale = Vector3.one;
    }
}