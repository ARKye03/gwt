using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The AudioManager class is responsible for managing audio within the game.
/// It ensures that the audio manager object persists across different scenes.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// The instance of the audio manager singleton.
    /// </summary>
    public static AudioManager _instance { get; private set; }
    /// <summary>
    /// The audio source for the audio manager.
    /// </summary>
    public AudioSource audioSource;
    /// <summary>
    /// The volume slider for the audio manager.
    /// </summary>
    public Slider volumeSlider;
    /// <summary>
    /// The PlayerPrefs key for the volume value.
    /// </summary>
    private const string VolumePrefKey = "Volume";
    /// <summary>
    /// Ensures that only one instance of the audio manager exists. 
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
        // Simplest method to persist object through scenes
        DontDestroyOnLoad(gameObject);

        // Load the saved volume value
        LoadVolume();
    }
    /// <summary>
    /// Adds a listener to the volume slider.
    /// </summary>
    void Start()
    {
        // Add listener to the volume slider
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    /// <summary>
    /// Called when the volume slider value changes.
    /// </summary>
    /// <param name="value">The new volume value.</param>
    private void OnVolumeChanged(float value)
    {
        audioSource.volume = value;
        SaveVolume(value);
    }

    /// <summary>
    /// Saves the volume value to PlayerPrefs.
    /// </summary>
    /// <param name="value">The volume value to save.</param>
    private void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads the volume value from PlayerPrefs.
    /// </summary>
    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            float volume = PlayerPrefs.GetFloat(VolumePrefKey);
            audioSource.volume = volume;
            volumeSlider.value = volume;
        }
        else
        {
            // Set default volume if no saved value exists
            audioSource.volume = 0.3f;
            volumeSlider.value = 0.3f;
        }
    }
}