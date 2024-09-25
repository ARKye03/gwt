using UnityEngine;

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
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }
}
