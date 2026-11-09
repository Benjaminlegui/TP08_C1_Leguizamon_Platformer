using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Context ctx;
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;
    
    public virtual bool CanMove => true;
    public virtual bool CanJump => true;
    protected Character character => ctx.Character;
    protected Rigidbody2D body => character.Body;
    protected Animator animator => character.Animator;
    protected bool isGrounded => character.IsGrounded;
    protected Vector2 moveDirection => character.MoveDirection;
    
    public void SetContext(Context context)
    {
        ctx = context;
    }
    
    public void Initialize()
    {
        isComplete = false;
        startTime = Time.time;
    }
    
    public virtual void Enter() {}
    public virtual void Do() {}
    public virtual void FixedDo() {}
    public virtual void Exit() {}
    
    public virtual void DoBranch() { Do(); }
    public virtual void FixedDoBranch() { FixedDo(); }
    public virtual void ExitBranch() { Exit(); }
    
    protected void Play(AnimationClip clip, float normalizedTime = 0f, float speed = 1f)
    {
        animator.speed = speed;
        animator.Play(clip.name, 0, normalizedTime);
    }

    protected bool IsPlaying(AnimationClip clip)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(clip.name);
    }

    protected bool IsFinished(AnimationClip clip)
    {
        return Progress(clip) >= 1;
    }

    protected float Progress(AnimationClip clip)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return IsPlaying(clip) ? info.normalizedTime : 0f;
    }
}
