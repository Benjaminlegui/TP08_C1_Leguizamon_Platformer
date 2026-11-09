using System;
using UnityEngine;

public class RunTimer : MonoBehaviour
{
    private const string BestTimePrefsKey = "Platformer.BestTime";

    [SerializeField] private GameManager gameManager;

    public float ElapsedTime { get; private set; }
    public float BestTime { get; private set; }
    public bool HasBestTime => BestTime > 0f;
    public bool IsNewBestTime { get; private set; }
    public event Action Changed;

    private void Awake()
    {
        BestTime = PlayerPrefs.GetFloat(BestTimePrefsKey, 0f);
    }

    private void OnEnable() => gameManager.Changed += OnGameChanged;
    private void OnDisable() => gameManager.Changed -= OnGameChanged;

    private void Update()
    {
        if (gameManager.IsPlaying)
            ElapsedTime += Time.deltaTime;
    }

    private void OnGameChanged()
    {
        if (gameManager.State != GameManager.GameState.Won) return;
        if (HasBestTime && ElapsedTime >= BestTime) return;

        BestTime = ElapsedTime;
        IsNewBestTime = true;
        PlayerPrefs.SetFloat(BestTimePrefsKey, BestTime);
        PlayerPrefs.Save();
        Changed?.Invoke();
    }

    public static string Format(float seconds)
    {
        int total = (int)seconds;
        return $"{total / 60:00}:{total % 60:00}";
    }

    public static string FormatPrecise(float seconds)
    {
        return $"{(int)seconds / 60:00}:{seconds % 60f:00.00}";
    }
}
