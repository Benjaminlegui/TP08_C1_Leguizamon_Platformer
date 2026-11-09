using UnityEngine;

public class HealthPickup : Pickup
{
    protected override bool TryApply(Player player)
    {
        HealState healState = player.GetComponentInChildren<HealState>(true);

        return healState != null && healState.AddCharge();
    }
}
