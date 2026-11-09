using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField, Min(1)] private int healAmount = 25;

    protected override bool TryApply(Player player)
    {
        Health health = player.GetComponent<Health>();

        if (health.CurrentHealth >= health.MaxHealth)
            return false;

        health.Heal(healAmount);
        return true;
    }
}
