using UnityEngine;

/// <summary>
/// The AudioManager class is responsible for managing audio within the game.
/// It ensures that the audio manager object persists across different scenes.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance { get; private set; }
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
        // Simplest method to persist object through scenes
        DontDestroyOnLoad(gameObject);
    }
}
