using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    private float invulnerabilityAfterHit;
    private float invulnerableUntil;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;
    public bool IsInvulnerable => Time.time < invulnerableUntil;
    public event Action Changed;
    public event Action Died;
    public event Action Damaged;
    public event Action Healed;
    public event Action Blocked;

    public void Initialize(int max, float invulnerabilityAfterHit = 0f)
    {
        maxHealth = max;
        currentHealth = maxHealth;
        this.invulnerabilityAfterHit = invulnerabilityAfterHit;
        Changed?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        if (IsDead || amount <= 0) return;

        if (IsInvulnerable)
        {
            Blocked?.Invoke();
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - amount);
        Changed?.Invoke();
        Damaged?.Invoke();

        if (IsDead)
            Died?.Invoke();
        else
            SetInvulnerable(invulnerabilityAfterHit);
    }

    public void Heal(int amount)
    {
        if (IsDead || amount <= 0) return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        Changed?.Invoke();
        Healed?.Invoke();
    }

    public void Kill()
    {
        if (IsDead) return;

        currentHealth = 0;
        Changed?.Invoke();
        Died?.Invoke();
    }

    public void SetInvulnerable(float duration)
    {
        invulnerableUntil = Mathf.Max(invulnerableUntil, Time.time + duration);
    }

    [ContextMenu("Take 10 Damage")]
    private void TakeTestDamage() => TakeDamage(10);
}
