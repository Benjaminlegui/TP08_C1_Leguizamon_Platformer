using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum VolumeChannel { Master, Music, Effects }

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
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip sceneMusic;

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
        musicSource.playOnAwake = false;
        effectsSource.playOnAwake = false;
        musicSource.loop = true;
        musicSource.spatialBlend = effectsSource.spatialBlend = 0f;
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

    public float GetVolume(VolumeChannel channel) => channel switch
    {
        VolumeChannel.Music => MusicVolume,
        VolumeChannel.Effects => EffectsVolume,
        _ => MasterVolume
    };

    public void SetVolume(VolumeChannel channel, float value)
    {
        switch (channel)
        {
            case VolumeChannel.Master: SetMasterVolume(value); break;
            case VolumeChannel.Music: SetMusicVolume(value); break;
            case VolumeChannel.Effects: SetEffectsVolume(value); break;
        }
    }

    private void ApplyVolume(string parameter, float value)
    {
        float decibels = value <= 0f ? -80f : Mathf.Log10(value) * 20f;
        if (!audioMixer.SetFloat(parameter, decibels))
            Debug.LogError("Missing exposed AudioMixer parameter: " + parameter, this);
    }
}
