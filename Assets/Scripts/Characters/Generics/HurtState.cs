using UnityEngine;

public class HurtState : State
{
    [SerializeField] private AnimationClip hurtClip;
    public override bool CanMove => false;
    public override bool CanJump => false;

    public override void Enter()
    {
        Play(hurtClip);
    }

    public override void Do()
    {
        if (IsFinished(hurtClip))
            isComplete = true;
    }
}
