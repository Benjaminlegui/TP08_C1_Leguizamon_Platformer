using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource effectsSource;

    private static FeedbackPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    public static void Play(PooledEffect effect, AudioClip clip, Vector3 position)
    {
        if (effect != null)
            PoolRegistry.Get(effect, position);

        PlaySound(clip);
    }

    public static void PlaySound(AudioClip clip)
    {
        if (clip != null && instance != null)
            instance.effectsSource.PlayOneShot(clip);
    }
}
