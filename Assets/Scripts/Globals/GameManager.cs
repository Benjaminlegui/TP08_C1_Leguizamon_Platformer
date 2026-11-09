using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, GameOver }

    [SerializeField] private Player player;

    private Health playerHealth;

    public GameState State { get; private set; } = GameState.Playing;
    public bool IsPlaying => State == GameState.Playing;
    public event Action Changed;

    private void Awake() => playerHealth = player.GetComponent<Health>();

    private void OnEnable() => playerHealth.Died += EndGame;
    private void OnDisable() => playerHealth.Died -= EndGame;

    public void EndGame()
    {
        if (!IsPlaying) return;
        State = GameState.GameOver;
        player.enabled = false;
        Time.timeScale = 0f;
        Changed?.Invoke();
    }

    public void RestartGame() => SceneLoader.LoadGameplay();
}
