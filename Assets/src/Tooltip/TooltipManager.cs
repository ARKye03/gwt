using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance;
    public TextMeshProUGUI textUI;
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
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.position = Input.mousePosition;
    }
    public void ShowTooltip(string msg)
    {
        gameObject.SetActive(true);
        textUI.text = msg;
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
        textUI.text = string.Empty;
    }
}
