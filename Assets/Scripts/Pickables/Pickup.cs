using UnityEngine;

public abstract class Pickup : Spawn
{
    [SerializeField] private PooledEffect pickupEffect;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float groundOffset = 0.2f;

    private Vector2 velocity;
    private bool falling;

    public void Launch(Vector2 initialVelocity)
    {
        velocity = initialVelocity;
        falling = true;
    }

    public override void OnRelease()
    {
        velocity = Vector2.zero;
        falling = false;
    }

    private void Update()
    {
        if (!falling) return;

        velocity.y -= gravity * Time.deltaTime;
        Vector2 step = velocity * Time.deltaTime;

        if (step.y < 0f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundOffset - step.y, groundLayer);

            if (hit)
            {
                transform.position = hit.point + Vector2.up * groundOffset;
                falling = false;
                return;
            }
        }

        transform.position += (Vector3)step;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player == null || !TryApply(player))
            return;

        FeedbackPlayer.Play(pickupEffect, pickupSound, transform.position);
        Despawn();
    }

    protected abstract bool TryApply(Player player);
}
