using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
public class Player : Context
{
    [SerializeField] private AirState airState;
    [SerializeField] private GroundState groundState;
    [SerializeField] private HurtState hurtState;
    [SerializeField] private DeathState deathState;

    private bool jumpRequested;
    
    [Range(0.1f, 0.8f)]
    [SerializeField] private float airSpeedMultiplier = 0.6f;
    [Min(0)]
    [SerializeField] private int airJumps = 0;

    private int airJumpsUsed;

    public event System.Action Died;

    public void UnlockAirJumps(int count)
    {
        airJumps = Mathf.Max(airJumps, count);
    }

    private void Start()
    {
        machine.Set(groundState);
    }

    private void OnEnable()
    {
        health.Damaged += OnDamaged;
        health.Died += OnDied;
        deathState.Finished += OnDeathFinished;
    }

    private void Update()
    {
        if (machine.state.CanMove)
            character.Face(character.MoveDirection.x);
        
        if (machine.state.isComplete)
            SelectState();
        
        machine.Do();

        character.AttackRequested = false;
        character.HealRequested = false;
    }

    private void FixedUpdate()
    {
        float moveX = machine.state.CanMove ? character.MoveDirection.x : 0f;
        float xVelocity = isGrounded ? moveX * character.Speed : moveX * (character.Speed * airSpeedMultiplier);
        
        if (isGrounded)
            airJumpsUsed = 0;

        if (jumpRequested && machine.state.CanJump)
        {
            if (isGrounded)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, airState.JumpVelocity);
            }
            else if (airJumpsUsed < airJumps)
            {
                airJumpsUsed++;
                body.linearVelocity = new Vector2(body.linearVelocity.x, airState.JumpVelocity);
                airState.AirJump();
            }
        }
        
        jumpRequested = false;
        
        body.linearVelocity = new Vector2(xVelocity, body.linearVelocity.y);
        
        machine.FixedDo();
    }
    
    private void OnDisable()
    {
        health.Damaged -= OnDamaged;
        health.Died -= OnDied;
        deathState.Finished -= OnDeathFinished;
    }

    private void OnMove(InputValue value)
    {
        character.MoveDirection = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if( value.isPressed )
            jumpRequested = true;
    }
    
    private void OnAttack(InputValue value)
    {
        if (value.isPressed)
            character.AttackRequested = true;
    }

    private void OnHeal(InputValue value)
    {
        if (value.isPressed)
            character.HealRequested = true;
    }

    private void OnDamaged()
    {
        if (health.IsDead)
            return;

        machine.Set(hurtState);
    }

    private void OnDied()
    {
        machine.Set(deathState);
    }

    private void OnDeathFinished()
    {
        Died?.Invoke();
    }
    
    private void SelectState()
    {
        if (isGrounded)
            machine.Set(groundState);
        else
            machine.Set(airState);
    }
}
