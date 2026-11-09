using UnityEngine;

public class AudioGameplay : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player player;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip loseSound;

    // private void Start()
    // {
    //     gameManager.Changed += OnGameChanged;
    //     player.Jumped += PlayJump;
    //     player.Landed += PlayLanding;
    //     player.Threw += PlayThrow;
    //
    //     audioManager.PlayMusic(gameplayMusic);
    // }
    //
    // private void OnDestroy()
    // {
    //     if (gameManager != null) gameManager.Changed -= OnGameChanged;
    //
    //     if (player != null)
    //     {
    //         player.Jumped -= PlayJump;
    //         player.Landed -= PlayLanding;
    //         player.Threw -= PlayThrow;
    //     }
    // }

    private void OnGameChanged()
    {
        if (gameManager.State != GameManager.GameState.GameOver) return;

        audioManager.StopMusic();
        audioManager.StopEffects();
        audioManager.PlayEffect(loseSound);
    }

    private void PlayJump() => audioManager.PlayEffect(jumpSound);
    private void PlayLanding() => audioManager.PlayEffect(landingSound);
    private void PlayThrow() => audioManager.PlayEffect(throwSound);
}
