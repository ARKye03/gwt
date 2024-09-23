using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFX : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlaySoundOnHover()
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySoundOnHover();
    }

}
