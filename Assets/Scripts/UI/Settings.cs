/* 
 * Settings.cs
 * Marlow Greenan
 * 3/30/2025
 */
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterAudioSlider;
    [SerializeField] private Slider _ambientVolumeSlider;
    [SerializeField] private Slider _SFXVolumeSlider;

    public void Start()
    {
        _masterAudioSlider.value = StaticData.VolumeMaster;
        _ambientVolumeSlider.value = StaticData.VolumeAmbient;
        _SFXVolumeSlider.value = StaticData.VolumeSFX;
    }

    /// <summary>
    /// Changes sound volumes.
    /// </summary>
    /// <param name="audioGroup"></param>
    public void ChangeSound(AudioMixerGroup audioGroup)
    {
        float volume = 0;
        switch (audioGroup.name)
        {
            case "Master":
                volume = _masterAudioSlider.value;
                _masterAudioSlider.transform.GetChild(0).GetComponent<TMP_Text>().text = Mathf.RoundToInt(_masterAudioSlider.value * 100).ToString() + "%";
                StaticData.VolumeMaster = volume;
                _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
                break;
            case "Ambient":
                volume = _ambientVolumeSlider.value;
                StaticData.VolumeAmbient = volume;
                _ambientVolumeSlider.transform.GetChild(0).GetComponent<TMP_Text>().text = Mathf.RoundToInt(_ambientVolumeSlider.value * 100).ToString() + "%";
                _audioMixer.SetFloat("Ambient", Mathf.Log10(volume) * 20);
                break;
            case "SFX":
                volume = _SFXVolumeSlider.value;
                StaticData.VolumeSFX = volume;
                _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
                _SFXVolumeSlider.transform.GetChild(0).GetComponent<TMP_Text>().text = Mathf.RoundToInt(_SFXVolumeSlider.value * 100).ToString() + "%";
                AudioManager.Instance.PlayButtonClick();
                break;
        }
    }
    public void Back()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
