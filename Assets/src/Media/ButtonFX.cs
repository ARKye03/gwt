using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The ButtonFX class is responsible for playing a sound effect when a pointer enters the button area.
/// Implements the <see cref="IPointerEnterHandler"/> interface to handle pointer enter events.
/// </summary>
public class ButtonFX : MonoBehaviour, IPointerEnterHandler
{
    /// <summary>
    /// The AudioSource component used to play the audio clip.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// The AudioClip to be played when the pointer enters the button area.
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// Plays the assigned audio clip using the AudioSource component.
    /// This method is called when the pointer enters the button area.
    /// </summary>
    public void PlaySoundOnHover()
    {
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// Called when the pointer enters the button area.
    /// Triggers the PlaySoundOnHover method.
    /// </summary>
    /// <param name="eventData">Current event data.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySoundOnHover();
    }
}
