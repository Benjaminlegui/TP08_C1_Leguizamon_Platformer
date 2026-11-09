using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum UISound { Click, Hover, Back, Pause, Unpause }

    #region MixerConstants
        private const string MasterVolumePrefsKey = "Platformer.MasterVolume";
        private const string MusicVolumePrefsKey = "Platformer.MusicVolume";
        private const string EffectsVolumePrefsKey = "Platformer.EffectsVolume";

        private const string MasterVolumeMixerParameter = "MasterVolume";
        private const string MusicVolumeMixerParameter = "MusicVolume";
        private const string EffectsVolumeMixerParameter = "EffectsVolume";
    #endregion

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip sceneMusic;

    [Header("UI Sounds")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip backSound;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip unpauseSound;

    #region SettersGetters
        public float MasterVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }

    #endregion

    private void Awake()
    {
        MasterVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(MasterVolumePrefsKey, 0.2f));
        MusicVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(MusicVolumePrefsKey, 0.4f));
        EffectsVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(EffectsVolumePrefsKey, 1f));
        musicSource.playOnAwake = effectsSource.playOnAwake = uiSource.playOnAwake = false;
        musicSource.loop = true;
        musicSource.spatialBlend = effectsSource.spatialBlend = uiSource.spatialBlend = 0f;
        uiSource.ignoreListenerPause = true;
    }

    private void Start()
    {
        ApplyVolume(MasterVolumeMixerParameter, MasterVolume);
        ApplyVolume(MusicVolumeMixerParameter, MusicVolume);
        ApplyVolume(EffectsVolumeMixerParameter, EffectsVolume);
        if (sceneMusic != null) PlayMusic(sceneMusic);
    }

    private void OnDisable()
    {
        musicSource.Stop();
        effectsSource.Stop();
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayUI(UISound sound)
    {
        AudioClip clip = sound switch
        {
            UISound.Hover => hoverSound,
            UISound.Back => backSound,
            UISound.Pause => pauseSound,
            UISound.Unpause => unpauseSound,
            _ => clickSound
        };

        if (clip != null) uiSource.PlayOneShot(clip);
    }

    public void SetMasterVolume(float value)
    {
        MasterVolume = Mathf.Clamp01(value);
        ApplyVolume(MasterVolumeMixerParameter, MasterVolume);
        PlayerPrefs.SetFloat(MasterVolumePrefsKey, MasterVolume);
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = Mathf.Clamp01(value);
        ApplyVolume(MusicVolumeMixerParameter, MusicVolume);
        PlayerPrefs.SetFloat(MusicVolumePrefsKey, MusicVolume);
    }

    public void SetEffectsVolume(float value)
    {
        EffectsVolume = Mathf.Clamp01(value);
        ApplyVolume(EffectsVolumeMixerParameter, EffectsVolume);
        PlayerPrefs.SetFloat(EffectsVolumePrefsKey, EffectsVolume);
    }

    private void ApplyVolume(string parameter, float value)
    {
        float decibels = value <= 0f ? -80f : Mathf.Log10(value) * 20f;
        if (!audioMixer.SetFloat(parameter, decibels))
            Debug.LogError("Missing exposed AudioMixer parameter: " + parameter, this);
    }
}
