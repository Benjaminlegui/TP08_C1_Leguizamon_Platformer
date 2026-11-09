using UnityEngine;

public class CoinPickup : Pickup
{
    [SerializeField, Min(1)] private int amount = 1;

    protected override bool TryApply(Player player)
    {
        CoinWallet.Add(amount);
        return true;
    }
}
