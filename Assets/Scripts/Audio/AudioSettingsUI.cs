using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private TMP_Text masterPercentageText;
    [SerializeField] private TMP_Text musicPercentageText;
    [SerializeField] private TMP_Text effectsPercentageText;

    private void Start()
    {
        masterSlider.SetValueWithoutNotify(audioManager.MasterVolume);
        musicSlider.SetValueWithoutNotify(audioManager.MusicVolume);
        effectsSlider.SetValueWithoutNotify(audioManager.EffectsVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        RefreshPercentages();
    }

    private void SetMasterVolume(float value)
    {
        audioManager.SetMasterVolume(value);
        RefreshPercentages();
    }

    private void SetMusicVolume(float value)
    {
        audioManager.SetMusicVolume(value);
        RefreshPercentages();
    }

    private void SetEffectsVolume(float value)
    {
        audioManager.SetEffectsVolume(value);
        RefreshPercentages();
    }

    private void RefreshPercentages()
    {
        masterPercentageText.text = $"{Mathf.RoundToInt(masterSlider.value * 100f)}%";
        musicPercentageText.text = $"{Mathf.RoundToInt(musicSlider.value * 100f)}%";
        effectsPercentageText.text = $"{Mathf.RoundToInt(effectsSlider.value * 100f)}%";
    }

    private void OnDestroy()
    {
        if (masterSlider != null) masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        if (musicSlider != null) musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        if (effectsSlider != null) effectsSlider.onValueChanged.RemoveListener(SetEffectsVolume);
    }
}
