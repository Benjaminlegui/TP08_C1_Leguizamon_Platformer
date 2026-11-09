using UnityEngine;

public class AttackState : State
{
    [SerializeField] private AnimationClip attackClip;
    [Range(0f, 1f)]
    [SerializeField] private float cancelWindow = 0.5f;
    [Min(0.1f)]
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float hitRadius = 0.5f;
    [SerializeField] private Transform hitPoint;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int damage = 10;
    [Range(0f, 1f)] [SerializeField] private float hitTime = 0.5f;
    [SerializeField] private AudioClip attackSound;
    private bool hasHit;
    public bool CanHitTarget => FindTarget() != null;

    private float progress => Progress(attackClip);
    public override bool CanMove => false;
    public override bool CanJump => progress < cancelWindow;

    public override void Enter()
    {
        hasHit = false;
        Play(attackClip, 0f, attackSpeed);
        FeedbackPlayer.PlaySound(attackSound);
    }

    public override void Do()
    {
        if (!hasHit && progress >= hitTime)
        {
            hasHit = true;
            DealDamage();
        }
        if (!isGrounded || progress >= 1)
            isComplete = true;
    }

    public override void Exit()
    {
        animator.speed = 1f;
    }

    private Collider2D FindTarget()
    {
        return Physics2D.OverlapCircle(hitPoint.position, hitRadius, targetLayer);
    }

    private void DealDamage()
    {
        Collider2D collider = FindTarget();
        
        if (collider != null)
        {
            Health health = collider.GetComponentInParent<Health>();

            if (health != null)
                health.TakeDamage(Mathf.RoundToInt(damage * character.DamageMultiplier));
        }
    }

    private void OnDrawGizmos()
    {
        if (hitPoint == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
    }
}
