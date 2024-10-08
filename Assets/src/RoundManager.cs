using UnityEngine;
using TMPro;

/// <summary>
/// Manages the round count.
/// </summary>
public class RoundManager : MonoBehaviour
{
    /// <summary>
    /// The TextMeshProUGUI that displays the round count.
    /// </summary>
    public TextMeshProUGUI roundCount;

    /// <summary>
    /// The current round count.
    /// </summary>
    public int Round { get; private set; }

    /// <summary>
    /// Sets the count at the start of the game.
    /// </summary>
    private void Start()
    {
        UpdateCount();
    }
    /// <summary>
    /// Visually updates the round count.
    /// </summary>
    private void UpdateCount() => roundCount.text = $"{Round}";

    /// <summary>
    /// Increases the round count by one.
    /// </summary>
    public void IncreaseRound()
    {
        Round++;
        UpdateCount();
    }

    /// <summary>
    /// Resets the round count to zero.
    /// </summary>
    public void ResetRound()
    {
        Round = 0;
        UpdateCount();
    }

    /// <summary>
    /// Singleton instance of the RoundManager.
    /// </summary>
    public static RoundManager _instance { get; private set; }

    /// <summary>
    /// Ensures that there is only one instance of the RoundManager.
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
}