using UnityEngine;

public abstract class Context : MonoBehaviour
{
    protected Character character;
    protected Health health;
    public Character Character => character;
    
    protected Rigidbody2D body => character.Body;
    protected bool isGrounded => character.IsGrounded;

    public StateMachine machine { get; private set; }

    
    protected virtual void Awake()
    {
        character = GetComponent<Character>();
        health = GetComponent<Health>();
        machine = new StateMachine();
        
        State[] allChildStates = GetComponentsInChildren<State>(true);
        foreach (State childState in allChildStates)
        {
            childState.SetContext(this);
        }
    }
}
