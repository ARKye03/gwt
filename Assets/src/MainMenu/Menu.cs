using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the main menu logic.
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// The dropdown for the ally deck.
    /// </summary>
    public TMP_Dropdown allyDeck;
    /// <summary>
    /// The dropdown for the enemy deck.
    /// </summary>
    public TMP_Dropdown enemyDeck;
    /// <summary>
    /// Loads the next Game Scene, in this case the gwt scene.
    /// </summary>
    public void StartGame()
    {
        GameData.AllyPlayerDeckIndex = allyDeck.value;
        GameData.EnemyPlayerDeckIndex = enemyDeck.value;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
