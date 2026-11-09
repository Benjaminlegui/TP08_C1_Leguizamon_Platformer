using UnityEngine;

[RequireComponent(typeof(Character))]
public class BaseEnemy : Context
{
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private CombatState combatState;
    [SerializeField] private HurtState hurtState;
    
    private EnemySensor sensor;
    public Transform Target => sensor.Target;
    public float DirectionToTarget => Mathf.Sign(Target.position.x - transform.position.x);
    public bool CanSeeTarget => Target != null;
    
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
    }

    private void OnDamaged()
    {
        machine.Set(hurtState, true);
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
        if (machine.state == hurtState)
            return;
        
        machine.Set(combatState);
    }

    private void OnPlayerLost()
    {
        if (machine.state == hurtState)
            return;
        
        machine.Set(patrolState);
    }
}
