using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, GameOver, Won }

    [SerializeField] private Player player;

    public GameState State { get; private set; } = GameState.Playing;
    public bool IsPlaying => State == GameState.Playing;
    public event Action Changed;

    private void OnEnable() => player.Died += EndGame;
    private void OnDisable() => player.Died -= EndGame;

    public void EndGame() => Finish(GameState.GameOver);

    public void WinGame() => Finish(GameState.Won);

    public void RestartGame() => SceneLoader.LoadGameplay();

    private void Finish(GameState state)
    {
        if (!IsPlaying) return;
        State = state;
        player.enabled = false;
        Time.timeScale = 0f;
        Changed?.Invoke();
    }
}
