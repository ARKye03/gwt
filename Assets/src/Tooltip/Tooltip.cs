using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string msg;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager._instance.ShowTooltip(msg);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager._instance.HideTooltip();
    }
}
