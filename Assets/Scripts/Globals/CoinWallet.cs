using System;
using UnityEngine;

public static class CoinWallet
{
    private const string CoinsPrefsKey = "Platformer.Coins";

    public static event Action Changed;
    public static int Coins => PlayerPrefs.GetInt(CoinsPrefsKey, 0);

    public static void Add(int amount)
    {
        if (amount <= 0) return;

        PlayerPrefs.SetInt(CoinsPrefsKey, Coins + amount);
        Changed?.Invoke();
    }
}
