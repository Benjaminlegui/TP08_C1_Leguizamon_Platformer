using UnityEngine;

public class AirJumpPickup : Pickup
{
    [SerializeField, Min(1)] private int airJumps = 1;

    protected override bool TryApply(Player player)
    {
        player.UnlockAirJumps(airJumps);
        return true;
    }
}
