/* 
 * AudioManager.cs
 * Marlow Greenan
 * 3/29/2025
 */
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("UI")]
    [SerializeField] private AudioSource _UIAudio;
    [SerializeField] private AudioClip _buttonSFX;

    [Header("Interact")]
    [SerializeField] private AudioSource _InteractAudio;
    [SerializeField] private AudioClip _pickUpAudioSFX;

    public static AudioManager Instance { get => instance; set => instance = value; }

    private void Start()
    {
        instance = this;
    }

    /// <summary>
    /// Plays button audio.
    /// </summary>
    public void PlayButtonClick()
    {
        _UIAudio.clip = _buttonSFX;
        _UIAudio.Play();
    }

    /// <summary>
    /// Plays pickup sound.
    /// </summary>
    public void PlayPickUpAudio()
    {
        _InteractAudio.clip = _pickUpAudioSFX;
        _InteractAudio.Play();
    }
}
