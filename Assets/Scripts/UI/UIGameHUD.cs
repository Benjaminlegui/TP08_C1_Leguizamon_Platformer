using TMPro;
using UnityEngine;

public class UIGameHUD : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text killsText;

    private float elapsedTime;
    private int shownSeconds = -1;
    private int kills;

    private void OnEnable()
    {
        // Enemy.Died += AddKill;
        RefreshKills();
        RefreshTime();
    }

    private void OnDisable()
    {
        // Enemy.Died -= AddKill;
    }

    private void Update()
    {
        if (!gameManager.IsPlaying)
            return;

        elapsedTime += Time.deltaTime;
        RefreshTime();
    }

    private void AddKill()
    {
        kills++;
        RefreshKills();
    }

    private void RefreshTime()
    {
        int seconds = (int)elapsedTime;

        if (seconds == shownSeconds)
            return;

        shownSeconds = seconds;
        timeText.text = $"{seconds / 60:00}:{seconds % 60:00}";
    }

    private void RefreshKills()
    {
        killsText.text = $"Kills: {kills}";
    }
}
