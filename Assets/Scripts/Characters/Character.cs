using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private CharacterConfigSo config;

    private float damageMultiplier = 1f;
    private float damageBoostUntil;

    public Rigidbody2D Body { get; private set; }
    public Animator Animator { get; private set; }
    public Health Health { get; private set; }
    public Vector2 MoveDirection { get; set; }
    
    public bool AttackRequested { get; set; }
    public bool HealRequested { get; set; }
    public bool IsGrounded => groundCheck.IsGrounded;
    public float FacingDirection => transform.right.x >= 0f ? 1f : -1f;

    public bool FrontGrounded => FacingDirection > 0 ? groundCheck.RightGrounded : groundCheck.LeftGrounded;
    public float Speed => config.Speed;
    public int ContactDamage => config.ContactDamage;
    public CharacterConfigSo Config => config;
    public float DamageMultiplier => Time.time < damageBoostUntil ? damageMultiplier : 1f;

    private void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        Health = GetComponent<Health>();
    }

    private void Start()
    {
        if (Health != null)
        {
            Health.Initialize(config.MaxHealth, config.InvulnerabilityAfterHit);
        }    
    }
    
    public void Face(float direction)
    {
        // Warning: comparacion de float con ==. Con un stick analogico el valor puede quedar en 0.0001 y el personaje gira igual.
        // Usa Mathf.Approximately(direction, 0f) o un umbral (Mathf.Abs(direction) < 0.01f).
        if (direction == 0f)
            return;

        float angleY = direction > 0f ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }

    public void ApplyDamageBoost(float multiplier, float duration)
    {
        damageMultiplier = multiplier;
        damageBoostUntil = Time.time + duration;
    }
}
