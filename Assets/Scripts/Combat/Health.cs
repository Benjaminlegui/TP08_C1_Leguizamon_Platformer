using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;
    public event Action Changed;
    public event Action Died;
    public event Action Damaged;

    public void Initialize(int max)
    {
        maxHealth = max;
        currentHealth = maxHealth;
        Changed?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        if (IsDead || amount <= 0) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);
        Changed?.Invoke();
        Damaged?.Invoke();

        if (IsDead) Died?.Invoke();
    }

    public void Heal(int amount)
    {
        if (IsDead || amount <= 0) return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        Changed?.Invoke();
    }

    [ContextMenu("Take 10 Damage")]
    private void TakeTestDamage() => TakeDamage(10);
}
