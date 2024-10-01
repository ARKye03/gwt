using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the main menu logic.
/// </summary>
public class Menu : MonoBehaviour
{
    public TMP_Dropdown allyDeck;
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
