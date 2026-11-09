using System;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class BaseEnemy : Context
{
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private CombatState combatState;
    [SerializeField] private HurtState hurtState;
    [SerializeField] private DeathState deathState;

    private EnemySensor sensor;
    public Transform Target => sensor.Target;
    public float DirectionToTarget => Mathf.Sign(Target.position.x - transform.position.x);
    public bool CanSeeTarget => Target != null;

    public static event Action Killed;

    protected override void Awake()
    {
        base.Awake();
        sensor = GetComponentInChildren<EnemySensor>();
    }

    private void OnEnable()
    {
        sensor.PlayerDetected += OnPlayerDetection;
        sensor.PlayerLost += OnPlayerLost;
        health.Damaged += OnDamaged;
        health.Died += OnDied;
        deathState.Finished += OnDeathFinished;
    }
    
    private void Start()
    {
        machine.Set(patrolState);
    }

    private void Update()
    {
        if (machine.state.CanMove)
            character.Face(character.MoveDirection.x);
        
        if (machine.state.isComplete)
            SelectState();
        
        machine.Do();
    }

    private void FixedUpdate()
    {
        float moveX = machine.state.CanMove ? character.MoveDirection.x : 0f;
        float xVelocity = moveX * character.Speed;
        
        body.linearVelocity = new Vector2(xVelocity, body.linearVelocity.y);
        
        machine.FixedDo();
    }

    private void OnDisable()
    {
        sensor.PlayerDetected -= OnPlayerDetection;
        sensor.PlayerLost -= OnPlayerLost;
        health.Damaged -= OnDamaged;
        health.Died -= OnDied;
        deathState.Finished -= OnDeathFinished;
    }

    private void OnDamaged()
    {
        if (health.IsDead)
            return;

        machine.Set(hurtState, true);
    }

    private void OnDied()
    {
        machine.Set(deathState);
        Killed?.Invoke();
    }

    private void OnDeathFinished()
    {
        gameObject.SetActive(false);
    }
    
    protected virtual void SelectState()
    {
        if (CanSeeTarget)
        {
            machine.Set(combatState);
        }
        else
        {
            machine.Set(patrolState);
        }
    }

    private void OnPlayerDetection()
    {
        if (machine.state == hurtState || health.IsDead)
            return;
        
        machine.Set(combatState);
    }

    private void OnPlayerLost()
    {
        if (machine.state == hurtState || health.IsDead)
            return;
        
        machine.Set(patrolState);
    }
}
