using System;
using UnityEngine;

public class HealState : State
{
    [SerializeField] private AnimationClip healClip;
    [Min(0.1f)]
    [SerializeField] private float healSpeed = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float healTime = 0.6f;
    [Min(1)]
    [SerializeField] private int healAmount = 25;
    [Min(0)]
    [SerializeField] private int maxCharges = 3;
    [Min(0)]
    [SerializeField] private int startCharges = 0;

    private int charges;
    private bool hasHealed;

    public event Action ChargesChanged;

    public int Charges => charges;
    public int MaxCharges => maxCharges;
    public bool CanHeal => charges > 0 && character.Health.CurrentHealth < character.Health.MaxHealth;

    public override bool CanMove => false;
    public override bool CanJump => false;

    private float progress => Progress(healClip);

    private void Start()
    {
        SetCharges(startCharges);
    }

    public bool AddCharge()
    {
        if (charges >= maxCharges)
            return false;

        SetCharges(charges + 1);
        return true;
    }

    public void RefillCharges()
    {
        SetCharges(maxCharges);
    }

    public override void Enter()
    {
        hasHealed = false;
        Play(healClip, 0f, healSpeed);
    }

    public override void Do()
    {
        if (!hasHealed && progress >= healTime)
        {
            hasHealed = true;
            SetCharges(charges - 1);
            character.Health.Heal(healAmount);
        }

        if (!isGrounded || progress >= 1)
            isComplete = true;
    }

    public override void Exit()
    {
        animator.speed = 1f;
    }

    private void SetCharges(int value)
    {
        charges = Mathf.Clamp(value, 0, maxCharges);
        ChargesChanged?.Invoke();
    }
}
