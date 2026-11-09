using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioManager.VolumeChannel channel;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text percentageText;

    private AudioManager audioManager;

    private void Awake() => audioManager = FindAnyObjectByType<AudioManager>();

    private void Start()
    {
        slider.SetValueWithoutNotify(audioManager.GetVolume(channel));
        slider.onValueChanged.AddListener(SetVolume);
        RefreshPercentage();
    }

    private void OnDestroy()
    {
        if (slider != null) slider.onValueChanged.RemoveListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        audioManager.SetVolume(channel, value);
        RefreshPercentage();
    }

    private void RefreshPercentage()
    {
        percentageText.text = $"{Mathf.RoundToInt(slider.value * 100f)}%";
    }
}
