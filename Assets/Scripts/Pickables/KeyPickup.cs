public class KeyPickup : Pickup
{
    protected override bool TryApply(Player player)
    {
        if (player.HasKey)
            return false;

        player.CollectKey();
        return true;
    }
}
