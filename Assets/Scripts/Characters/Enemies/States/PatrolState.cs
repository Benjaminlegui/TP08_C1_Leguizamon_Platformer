using System.Collections;
using UnityEngine;

public class PatrolState : ParentState
{
    [SerializeField] private MoveState moveState;
    [SerializeField] private IdleState idleState;

    private float waitTime = 2f;
    private float lastDirection;
    
    public override void Enter()
    {
        character.MoveDirection = new Vector2(character.FacingDirection, 0f);
        machine.Set(moveState);
    }
    
    public override void Do()
    {
        if (machine.state == idleState &&  idleState.time >= waitTime)
        {
            character.MoveDirection = new Vector2(-character.FacingDirection, 0f);
            machine.Set(moveState);
            return;
        }
        
        if (!character.FrontGrounded && character.IsGrounded)
        {
             character.MoveDirection = new Vector2(0f, 0f);
             machine.Set(idleState);
        }
    }
}
