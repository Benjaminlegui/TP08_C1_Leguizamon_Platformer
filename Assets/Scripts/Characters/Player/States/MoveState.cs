using UnityEngine;

public class MoveState : State
{
    [SerializeField] private AnimationClip moveClip;
    [SerializeField] private float clipBaseSpeed = 1f;
    
    public override void Enter()
    {
        Play(moveClip, 0f, character.Speed / clipBaseSpeed);
        isComplete = true;
    }
}
