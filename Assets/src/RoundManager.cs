using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public TextMeshProUGUI roundCount;
    public int Round { get; private set; }

    private void Start()
    {
        UpdateCount();
    }

    private void UpdateCount() => roundCount.text = $"{Round}";

    public void IncreaseRound()
    {
        Round++;
        UpdateCount();
    }

    public void ResetRound()
    {
        Round = 0;
        UpdateCount();
    }
}