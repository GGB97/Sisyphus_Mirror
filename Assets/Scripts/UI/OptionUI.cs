using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Slider maxVolume;
    public Slider bgmVolume;
    public Slider sfxVolume;

    [SerializeField] private TextMeshProUGUI maxVolumeText;
    [SerializeField] private TextMeshProUGUI bgmVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    private void Start()
    {
        maxVolume.value = SoundManager.Instance.maxBgmVolume;
        bgmVolume.value = SoundManager.Instance.bgmVolumePercent;
        sfxVolume.value = SoundManager.Instance.sfxVolumePercent;
        
    }

    public void MenuExitButton()
    {
        this.gameObject.SetActive(false);
    }

    public void MaxVolumeChange(float volume)
    {
        maxVolume.value = volume;
        maxVolumeText.text = volume.ToString("N2");
        SoundManager.Instance.maxBgmVolume = volume;
    }

    public void BgmVolumeChange(float volume)
    {
        bgmVolume.value = Mathf.Ceil(volume);
        bgmVolumeText.text = Mathf.Ceil(volume).ToString();
        SoundManager.Instance.bgmVolumePercent = volume;
    }

    public void SfxVolumeChange(float volume)
    {
        sfxVolume.value = Mathf.Ceil(volume);
        sfxVolumeText.text = Mathf.Ceil(volume).ToString();
        SoundManager.Instance.sfxVolumePercent = volume;
    }
}
