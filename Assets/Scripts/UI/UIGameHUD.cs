using TMPro;
using UnityEngine;

public class UIGameHUD : MonoBehaviour
{
    [SerializeField] private RunTimer runTimer;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text killsText;

    private int shownSeconds = -1;
    private int kills;

    private void OnEnable()
    {
        BaseEnemy.Killed += AddKill;
        RefreshKills();
        RefreshTime();
    }

    private void OnDisable()
    {
        BaseEnemy.Killed -= AddKill;
    }

    private void Update()
    {
        RefreshTime();
    }

    private void AddKill()
    {
        kills++;
        RefreshKills();
    }

    private void RefreshTime()
    {
        int seconds = (int)runTimer.ElapsedTime;

        if (seconds == shownSeconds)
            return;

        shownSeconds = seconds;
        timeText.text = RunTimer.Format(runTimer.ElapsedTime);
    }

    private void RefreshKills()
    {
        killsText.text = $"Kills: {kills}";
    }
}
