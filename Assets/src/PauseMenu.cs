using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu _instance { get; private set; }
    public GameObject pauseMenu;

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
    public void ReloadScene()
    // A - Can this be more simple?  
    // B - I guess we'll never know.
    {
        ResetBoardAndDecks();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GetBackToMainMenu()
    {
        ResetBoardAndDecks();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

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

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }
}
