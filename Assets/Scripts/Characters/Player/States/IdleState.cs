using UnityEngine;

public class IdleState : State
{
    [SerializeField] private AnimationClip idleClip;
    
    public override void Enter()
    {
        Play(idleClip);
        isComplete = true;
    }
}
