using UnityEngine;

public class DamagePickup : Pickup
{
    [SerializeField, Min(1f)] private float damageMultiplier = 1.5f;
    [SerializeField, Min(0.1f)] private float duration = 10f;

    protected override bool TryApply(Player player)
    {
        player.GetComponent<Character>().ApplyDamageBoost(damageMultiplier, duration);
        return true;
    }
}
