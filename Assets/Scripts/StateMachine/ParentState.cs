using UnityEngine;

public class ParentState : State
{
    protected StateMachine machine = new StateMachine();

    public override bool CanMove => machine.state != null ? machine.state.CanMove : true;
    public override bool CanJump => machine.state != null ? machine.state.CanJump : true;

    public override void DoBranch()
    {
        Do();
        machine.Do();
    }

    public override void FixedDoBranch()
    {
        FixedDo();
        machine.FixedDo();
    }

    public override void ExitBranch()
    {
        machine.Exit();
        Exit();
    }
}
