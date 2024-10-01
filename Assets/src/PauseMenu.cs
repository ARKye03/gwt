using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages the pause menu in the game.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the PauseMenu.
    /// </summary>
    public static PauseMenu _instance { get; private set; }
    /// <summary>
    /// The pause menu game object.
    /// </summary>
    public GameObject pauseMenu;
    /// <summary>
    /// Initializes the singleton instance of the PauseMenu.
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
    /// <summary>
    /// Reloads the current scene.
    /// </summary>
    public void ReloadScene()
    // A - Can this be more simple?  
    // B - I guess we'll never know.
    {
        ResetBoardAndDecks();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Returns to the main menu.
    /// </summary>
    public void GetBackToMainMenu()
    {
        ResetBoardAndDecks();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    /// <summary>
    /// Resets the board and decks to their initial state.
    /// </summary>
    private static void ResetBoardAndDecks()
    {
        var board = Board._instance;
        var cardsQuanto = CardsQuanto._instance;

        if (board == null || cardsQuanto == null)
        {
            Debug.LogError("Board or CardsQuanto instance is null");
            return;
        }

        // Clear and reinitialize cards
        cardsQuanto.Clear();
        cardsQuanto.InitializeCards();

        // Reinitialize decks
        board.decks = board.InitDecks(cardsQuanto);
    }
    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Resumes the game.
    /// </summary>
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }
}
