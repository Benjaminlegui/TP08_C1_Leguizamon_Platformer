using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum VolumeChannel { Master, Music, Effects, UI }
    public enum UISound { Click, Hover, Back, Pause, Unpause }

    #region MixerConstants
        private const string MasterVolumePrefsKey = "Platformer.MasterVolume";
        private const string MusicVolumePrefsKey = "Platformer.MusicVolume";
        private const string EffectsVolumePrefsKey = "Platformer.EffectsVolume";
        private const string UIVolumePrefsKey = "Platformer.UIVolume";

        private const string MasterVolumeMixerParameter = "MasterVolume";
        private const string MusicVolumeMixerParameter = "MusicVolume";
        private const string EffectsVolumeMixerParameter = "EffectsVolume";
        private const string UIVolumeMixerParameter = "UIVolume";
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
        public float UIVolume { get; private set; }

    #endregion

    private void Awake()
    {
        MasterVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(MasterVolumePrefsKey, 0.2f));
        MusicVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(MusicVolumePrefsKey, 0.4f));
        EffectsVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(EffectsVolumePrefsKey, 1f));
        UIVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(UIVolumePrefsKey, 1f));
        musicSource.playOnAwake = false;
        effectsSource.playOnAwake = false;
        musicSource.loop = true;
        musicSource.spatialBlend = effectsSource.spatialBlend = 0f;

        if (uiSource != null)
        {
            uiSource.playOnAwake = false;
            uiSource.spatialBlend = 0f;
            uiSource.ignoreListenerPause = true;
        }
    }

    private void Start()
    {
        ApplyVolume(MasterVolumeMixerParameter, MasterVolume);
        ApplyVolume(MusicVolumeMixerParameter, MusicVolume);
        ApplyVolume(EffectsVolumeMixerParameter, EffectsVolume);
        ApplyVolume(UIVolumeMixerParameter, UIVolume);
        if (sceneMusic != null) PlayMusic(sceneMusic);
    }

    private void OnDisable()
    {
        musicSource.Stop();
        effectsSource.Stop();
        PlayerPrefs.Save();
    }
    
    public void StopMusic() => musicSource.Stop();
    public void StopEffects() => effectsSource.Stop();

    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void PlayEffect(AudioClip clip)
    {
        if (clip != null) effectsSource.PlayOneShot(clip);
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

        if (clip != null && uiSource != null) uiSource.PlayOneShot(clip);
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

    public void SetUIVolume(float value)
    {
        UIVolume = Mathf.Clamp01(value);
        ApplyVolume(UIVolumeMixerParameter, UIVolume);
        PlayerPrefs.SetFloat(UIVolumePrefsKey, UIVolume);
    }

    public float GetVolume(VolumeChannel channel) => channel switch
    {
        VolumeChannel.Music => MusicVolume,
        VolumeChannel.Effects => EffectsVolume,
        VolumeChannel.UI => UIVolume,
        _ => MasterVolume
    };

    public void SetVolume(VolumeChannel channel, float value)
    {
        switch (channel)
        {
            case VolumeChannel.Master: SetMasterVolume(value); break;
            case VolumeChannel.Music: SetMusicVolume(value); break;
            case VolumeChannel.Effects: SetEffectsVolume(value); break;
            case VolumeChannel.UI: SetUIVolume(value); break;
        }
    }

    private void ApplyVolume(string parameter, float value)
    {
        float decibels = value <= 0f ? -80f : Mathf.Log10(value) * 20f;
        if (!audioMixer.SetFloat(parameter, decibels))
            Debug.LogError("Missing exposed AudioMixer parameter: " + parameter, this);
    }
}
