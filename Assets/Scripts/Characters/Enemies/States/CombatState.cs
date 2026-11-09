using UnityEngine;

public class CombatState : ParentState
{
    private BaseEnemy enemy => (BaseEnemy)ctx;
    
    [SerializeField] private MoveState moveState;
    [SerializeField] private IdleState idleState;
    [SerializeField] private AttackState attackState;

    private float lastAttackTime;

    public override void Enter()
    {
        character.MoveDirection = new Vector2(enemy.DirectionToTarget, 0);
        machine.Set(moveState);
    }

    public override void Do()
    {
        if (!enemy.CanSeeTarget)
            return;

        if (machine.state == attackState && !attackState.isComplete)
            return;

        if (attackState.CanHitTarget)
        {
            if (Time.time - lastAttackTime >= character.Config.AttackCooldown)
            {
                Attack();
            }
            else
            {
                StopChase();
            }
        }
        else
        {
            HandleChase();
        }
    }

    private void Attack()
    {
        FaceTargetAndStop();
        machine.Set(attackState, true);
        lastAttackTime = Time.time;
    }

    private void HandleChase()
    {
        if (!character.FrontGrounded && character.IsGrounded)
        {
            StopChase();
            return;
        }
        
        character.MoveDirection = new Vector2(enemy.DirectionToTarget, 0);
        machine.Set(moveState);
    }
    
    private void StopChase()
    {
        FaceTargetAndStop();
        machine.Set(idleState);
    }

    private void FaceTargetAndStop()
    {
        character.MoveDirection = new Vector2(0, 0);
        character.Face(enemy.DirectionToTarget);
    }
}
