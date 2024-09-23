using UnityEngine;

/// <summary>
/// The AudioManager class is responsible for managing audio within the game.
/// It ensures that the audio manager object persists across different scenes.
/// </summary>
public class AudioManager : MonoBehaviour
{
    void Awake()
    {
        // Simplest method to persist object through scenes
        DontDestroyOnLoad(gameObject);
    }
}
