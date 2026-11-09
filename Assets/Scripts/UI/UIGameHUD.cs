using TMPro;
using UnityEngine;

public class UIGameHUD : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text coinsText;

    private float elapsedTime;
    private int shownSeconds = -1;
    private int kills;

    private void OnEnable()
    {
        BaseEnemy.Killed += AddKill;
        CoinWallet.Changed += RefreshCoins;
        RefreshKills();
        RefreshCoins();
        RefreshTime();
    }

    private void OnDisable()
    {
        BaseEnemy.Killed -= AddKill;
        CoinWallet.Changed -= RefreshCoins;
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

    private void RefreshCoins()
    {
        if (coinsText != null)
            coinsText.text = $"Coins: {CoinWallet.Coins}";
    }
}
