using UnityEngine;
using UnityEngine.EventSystems;

public class HandPanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hiddenPosition;
    public Vector3 visiblePosition;
    public float transitionSpeed = 5f;

    private Vector3 targetPosition;
    private Vector3 targetScale;

    void Start()
    {
        targetPosition = hiddenPosition;
        transform.localPosition = hiddenPosition;
        targetScale = Vector3.one;
        transform.localScale = Vector3.one;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * transitionSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetPosition = visiblePosition;
        targetScale = new Vector3(1.4f, 1.4f, 1.4f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetPosition = hiddenPosition;
        targetScale = Vector3.one;
    }
}