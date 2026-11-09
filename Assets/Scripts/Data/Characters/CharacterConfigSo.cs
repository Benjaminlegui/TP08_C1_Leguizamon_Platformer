using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfigSo", menuName = "Data/Characters/CharacterConfigSo", order = 1)]
public class CharacterConfigSo : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private float speed = 1f;
    [Min(0f)]
    [SerializeField] private float jumpHeight = 3f;
    [Range(0.1f, 0.8f)]
    [SerializeField] private float airSpeedMultiplier = 0.6f;

    [Header("Health")]
    [Min(1)]
    [SerializeField] private int maxHealth = 100;
    [Min(0f)]
    [SerializeField] private float invulnerabilityAfterHit = 0f;

    [Header("Combat")]
    [Min(0)]
    [SerializeField] private int attackDamage = 10;
    [Min(0f)]
    [SerializeField] private float attackCooldown = 1f;
    [Min(0)]
    [SerializeField] private int contactDamage = 0;

    [Header("Healing")]
    [Min(1)]
    [SerializeField] private int healAmount = 25;
    [Min(0)]
    [SerializeField] private int maxHealCharges = 3;
    [Min(0)]
    [SerializeField] private int startHealCharges = 0;

    public float Speed => speed;
    public float JumpHeight => jumpHeight;
    public float AirSpeedMultiplier => airSpeedMultiplier;
    public int MaxHealth => maxHealth;
    public float InvulnerabilityAfterHit => invulnerabilityAfterHit;
    public int AttackDamage => attackDamage;
    public float AttackCooldown => attackCooldown;
    public int ContactDamage => contactDamage;
    public int HealAmount => healAmount;
    public int MaxHealCharges => maxHealCharges;
    public int StartHealCharges => startHealCharges;
}
