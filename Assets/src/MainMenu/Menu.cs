using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Menu is a MonoBehaviour that manages the main menu logic.
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Loads the next Game Scene, in this case the gwt scene.
    /// </summary>
    public void StartGame()
    {
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
