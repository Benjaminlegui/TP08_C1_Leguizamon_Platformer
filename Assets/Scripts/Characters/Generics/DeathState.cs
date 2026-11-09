using System;
using UnityEngine;

public class DeathState : State
{
    [SerializeField] private AnimationClip deathClip;
    [Min(0f)]
    [SerializeField] private float finishDelay = 1f;

    public event Action Finished;

    private bool frozen;
    private bool finished;

    public override bool CanMove => false;
    public override bool CanJump => false;

    public override void Enter()
    {
        frozen = false;
        finished = false;
        Play(deathClip);
    }

    public override void Do()
    {
        if (!frozen && isGrounded)
            Freeze();

        if (!finished && time >= deathClip.length + finishDelay)
        {
            finished = true;
            Finished?.Invoke();
        }
    }

    private void Freeze()
    {
        frozen = true;
        body.linearVelocity = Vector2.zero;
        body.bodyType = RigidbodyType2D.Kinematic;

        foreach (Collider2D collider in character.GetComponentsInChildren<Collider2D>())
            collider.enabled = false;
    }
}
