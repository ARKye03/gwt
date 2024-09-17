using UnityEngine;
using UnityEngine.EventSystems;

public class HandPanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hiddenPosition;
    public Vector3 visiblePosition;
    public float transitionSpeed = 5f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = hiddenPosition;
        transform.localPosition = hiddenPosition;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * transitionSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered");
        targetPosition = visiblePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited");
        targetPosition = hiddenPosition;
    }
}