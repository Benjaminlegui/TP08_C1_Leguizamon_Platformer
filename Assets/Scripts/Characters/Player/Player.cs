using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
public class Player : Context
{
    [SerializeField] private AirState airState;
    [SerializeField] private GroundState groundState;
    [SerializeField] private HurtState hurtState;
    
    private bool jumpRequested;
    
    [Range(0.1f, 0.8f)]
    [SerializeField] private float airSpeedMultiplier = 0.6f;

    private void Start()
    {
        machine.Set(groundState);
    }

    private void OnEnable()
    {
        health.Damaged += OnDamaged;
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
        
        if (isGrounded && jumpRequested && machine.state.CanJump)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, airState.JumpVelocity);
        }
        
        jumpRequested = false;
        
        body.linearVelocity = new Vector2(xVelocity, body.linearVelocity.y);
        
        machine.FixedDo();
    }
    
    private void OnDisable()
    {
        health.Damaged -= OnDamaged;
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
        machine.Set(hurtState);
    }
    
    private void SelectState()
    {
        if (isGrounded)
            machine.Set(groundState);
        else
            machine.Set(airState);
    }
}
