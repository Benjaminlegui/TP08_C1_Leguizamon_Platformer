using UnityEngine;
using Helpers;

public class AirState : State
{
    private enum AirPhase { Jump, JumpToFall, Fall }
    
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private AnimationClip jumpClip;
    [SerializeField] private AnimationClip jumpToFallClip;
    [SerializeField] private AnimationClip fallClip;
    
    private AirPhase phase;

    private const float landingTolerance = 0.1f;
    private float gravity => Mathf.Abs(Physics2D.gravity.y) * body.gravityScale;
    public float JumpVelocity => Mathf.Sqrt(2 * jumpHeight * gravity);
    private float apexVelocity => gravity * (jumpToFallClip.length / 2);
    
    public override void Enter()
    {
        if (body.linearVelocity.y > 0)
            SetPhase(AirPhase.Jump);
        else
            SetPhase(AirPhase.Fall);
    }

    public override void Do()
    {
        if (phase != AirPhase.Jump && body.linearVelocity.y > apexVelocity)
            SetPhase(AirPhase.Jump);
        
        switch (phase)
        {
            case AirPhase.Jump:
                UpdateJump();
                break;
            case AirPhase.JumpToFall:
                UpdateJumpToFall();
                break;
        }
        
        isComplete = isGrounded && body.linearVelocity.y <= landingTolerance;
    }
    
    private void UpdateJump()
    {
        if (body.linearVelocity.y <= apexVelocity)
        {
            SetPhase(AirPhase.JumpToFall);
            return;
        }
        
        float time = MathHelpers.AnimationMapping(body.linearVelocity.y, JumpVelocity, apexVelocity, 0, 0.99f,
            true);
        Play(jumpClip, time);
    }

    private void UpdateJumpToFall()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if (IsFinished(jumpToFallClip))
            SetPhase(AirPhase.Fall);
    }
    
    private void SetPhase(AirPhase newPhase)
    {
        phase = newPhase;
        Play(GetClip(newPhase));
    }
    
    private AnimationClip GetClip(AirPhase airPhase)
    {
        switch (airPhase)
        {
            case AirPhase.Jump: return jumpClip;
            case AirPhase.JumpToFall: return jumpToFallClip;
            default: return fallClip;
        }
    }
}
