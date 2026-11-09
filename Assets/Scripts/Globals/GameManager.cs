using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, GameOver }

    [SerializeField] private Player player;

    public GameState State { get; private set; } = GameState.Playing;
    public bool IsPlaying => State == GameState.Playing;
    public event Action Changed;

    private void OnEnable() => player.Died += EndGame;
    private void OnDisable() => player.Died -= EndGame;

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
