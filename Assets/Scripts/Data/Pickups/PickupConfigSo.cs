using UnityEngine;

[CreateAssetMenu(fileName = "PickupConfigSo", menuName = "Data/Pickups/PickupConfigSo", order = 1)]
public class PickupConfigSo : ScriptableObject
{
    [Header("Damage boost")]
    [Min(1f)]
    [SerializeField] private float damageMultiplier = 1.5f;
    [Min(0.1f)]
    [SerializeField] private float damageBoostDuration = 10f;

    [Header("Invulnerability")]
    [Min(0.1f)]
    [SerializeField] private float invulnerabilityDuration = 5f;

    public float DamageMultiplier => damageMultiplier;
    public float DamageBoostDuration => damageBoostDuration;
    public float InvulnerabilityDuration => invulnerabilityDuration;
}
