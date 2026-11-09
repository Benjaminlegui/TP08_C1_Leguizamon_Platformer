using UnityEngine;

public class MoveState : State
{
    [SerializeField] private AnimationClip moveClip;
    [SerializeField] private float clipBaseSpeed = 1f;
    [SerializeField] private AudioClip footstepSound;
    [Min(0.05f)]
    [SerializeField] private float footstepInterval = 0.3f;

    private float nextFootstepTime;

    public override void Enter()
    {
        Play(moveClip, 0f, character.Speed / clipBaseSpeed);
        nextFootstepTime = Time.time;
        isComplete = true;
    }

    public override void Do()
    {
        if (footstepSound == null || !isGrounded || Time.time < nextFootstepTime)
            return;

        FeedbackPlayer.PlaySound(footstepSound);
        nextFootstepTime = Time.time + footstepInterval;
    }
}
