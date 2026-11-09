using System;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private float belowTolerance = 0.5f;
    
    public event Action PlayerDetected;
    public event Action PlayerLost;
    public Transform Target { get; private set; }
    private bool playerInSight;

    private void Update()
    {
        Target = null;

        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (playerCollider != null)
        {
            float enemyFeet = bodyCollider.bounds.min.y;
            float playerFeet = playerCollider.bounds.min.y;

            if (playerFeet > enemyFeet - belowTolerance)
            {
                RaycastHit2D blocked =
                    Physics2D.Linecast(transform.position, playerCollider.bounds.center, groundLayer);

                if (!blocked)
                    Target = playerCollider.attachedRigidbody.transform;
            }
        }

        bool seesPlayer = Target != null;

        if (seesPlayer && !playerInSight)
        {
            PlayerDetected?.Invoke();
        } else if (!seesPlayer && playerInSight)
        {
            PlayerLost?.Invoke();
        }
        
        playerInSight = seesPlayer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,  detectionRange);
    }
}
