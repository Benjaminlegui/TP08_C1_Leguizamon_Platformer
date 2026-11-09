using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfigSo", menuName = "Data/Characters/CharacterConfigSo", order = 1)]
public class CharacterConfigSo : ScriptableObject
{
    [SerializeField] private float speed = 1f;
    [Min(1)]
    [SerializeField] private int maxHealth = 100;
    [Min(0f)]
    [SerializeField] private float invulnerabilityAfterHit = 0f;
    
    public float Speed => speed;
    public int MaxHealth => maxHealth;
    public float InvulnerabilityAfterHit => invulnerabilityAfterHit;
}
