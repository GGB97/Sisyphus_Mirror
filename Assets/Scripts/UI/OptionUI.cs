using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UI_Base
{
    UIManager _ui;

    public Slider maxVolume;
    public Slider bgmVolume;
    public Slider sfxVolume;

    [SerializeField] private TextMeshProUGUI maxVolumeText;
    [SerializeField] private TextMeshProUGUI bgmVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _ui = UIManager.Instance;
    }

    private void OnEnable()
    {
        _ui.AddActiveUI(gameObject);
    }

    private void OnDisable()
    {
        _ui.RemoveActiveUI(gameObject);
    }


    private void Start()
    {
        maxVolume.value = SoundManager.Instance.maxBgmVolume * 100f;
        bgmVolume.value = SoundManager.Instance.bgmVolumePercent;
        sfxVolume.value = SoundManager.Instance.sfxVolumePercent;

        gameObject.SetActive(false);
    }

    public override void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void MaxVolumeChange(float volume)
    {
        maxVolume.value = volume;
        maxVolumeText.text = volume.ToString("N0");
        SoundManager.Instance.maxBgmVolume = Mathf.Clamp((volume / 100), 0, 2);
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
