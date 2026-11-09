using UnityEngine;
using UnityEngine.Serialization;

public class GroundState : ParentState
{
    [SerializeField] private IdleState idleState;
    [FormerlySerializedAs("runState")] [SerializeField] private MoveState moveState;
    [SerializeField] private AttackState attackState;

    public override void Do()
    {
        isComplete = !isGrounded;
        if (isComplete)
            return;

        if (machine.state == null || machine.state.isComplete)
            SelectState();
    }

    private void SelectState()
    {
        if (character.AttackRequested)
            machine.Set(attackState);
        else if (moveDirection.x == 0)
            machine.Set(idleState);
        else
            machine.Set(moveState);
    }
}
