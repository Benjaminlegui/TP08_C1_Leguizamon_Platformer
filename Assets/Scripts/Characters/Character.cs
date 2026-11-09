using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private CharacterConfigSo config;

    public Rigidbody2D Body { get; private set; }
    public Animator Animator { get; private set; }
    public Health Health { get; private set; }
    public Vector2 MoveDirection { get; set; }
    
    public bool AttackRequested { get; set; }
    public bool IsGrounded => groundCheck.IsGrounded;
    public float FacingDirection => transform.right.x >= 0f ? 1f : -1f;

    public bool FrontGrounded => FacingDirection > 0 ? groundCheck.RightGrounded : groundCheck.LeftGrounded;
    public float Speed => config.Speed;

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
            Health.Initialize(config.MaxHealth);
        }    
    }
    
    public void Face(float direction)
    {
        if (direction == 0f)
            return;

        float angleY = direction > 0f ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }
}
