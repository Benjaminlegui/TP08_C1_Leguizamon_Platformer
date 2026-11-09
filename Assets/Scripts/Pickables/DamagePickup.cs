using UnityEngine;

public class DamagePickup : Pickup
{
    [SerializeField] private PickupConfigSo config;

    protected override bool TryApply(Player player)
    {
        player.GetComponent<Character>().ApplyDamageBoost(config.DamageMultiplier, config.DamageBoostDuration);
        return true;
    }
}
