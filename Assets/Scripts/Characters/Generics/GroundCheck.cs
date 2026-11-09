using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength = 0.1f;
    [SerializeField] private float skinInset = 0.05f;

    private Collider2D bodyCollider;
    public bool  IsGrounded { get; private set; }
    public bool LeftGrounded { get; private set; }
    public bool RightGrounded { get; private set; }

    private void Awake()
    {
        bodyCollider = GetComponentInChildren<Collider2D>();
    }

    private void FixedUpdate()
    {
        Bounds bounds = bodyCollider.bounds;
        
        Vector2 leftFoot = new Vector2(bounds.min.x + skinInset, bounds.min.y);
        Vector2 rightFoot = new Vector2(bounds.max.x - skinInset, bounds.min.y);
        
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot, Vector2.down, rayLength, groundLayer);
        
        IsGrounded = leftHit.collider != null || rightHit.collider != null;
        LeftGrounded = leftHit;
        RightGrounded = rightHit;
    }
    
    void OnDrawGizmos()
    {
        Collider2D c = bodyCollider != null ? bodyCollider : GetComponentInChildren<Collider2D>();
        if (c == null) return;

        Bounds b = c.bounds;
        Vector2 leftFoot  = new Vector2(b.min.x + skinInset, b.min.y);
        Vector2 rightFoot = new Vector2(b.max.x - skinInset, b.min.y);

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(leftFoot,  leftFoot  + Vector2.down * rayLength);
        Gizmos.DrawLine(rightFoot, rightFoot + Vector2.down * rayLength);
    }

}
