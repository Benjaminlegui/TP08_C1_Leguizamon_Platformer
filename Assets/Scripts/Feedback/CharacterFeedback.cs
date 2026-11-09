using UnityEngine;

[RequireComponent(typeof(Health))]
public class CharacterFeedback : MonoBehaviour
{
    [Header("Hit")]
    [SerializeField] private PooledEffect hitEffect;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private AudioClip blockedSound;
    [SerializeField] private Color hitColor = new Color(1f, 0.35f, 0.35f);

    [Header("Heal")]
    [SerializeField] private PooledEffect healEffect;
    [SerializeField] private AudioClip healSound;
    [SerializeField] private Color healColor = new Color(0.45f, 1f, 0.5f);

    [Header("Death")]
    [SerializeField] private PooledEffect deathEffect;
    [SerializeField] private AudioClip deathSound;

    [Header("Flash / Blink")]
    [SerializeField, Min(0.01f)] private float flashDuration = 0.12f;
    [SerializeField, Min(0.01f)] private float blinkInterval = 0.08f;
    [Range(0f, 1f)]
    [SerializeField] private float blinkAlpha = 0.35f;

    private Health health;
    private SpriteRenderer spriteRenderer;
    private Color baseColor;
    private Color flashColor;
    private float flashUntil;

    private Vector3 Center => spriteRenderer.bounds.center;

    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        baseColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        health.Damaged += OnDamaged;
        health.Healed += OnHealed;
        health.Died += OnDied;
        health.Blocked += OnBlocked;
    }

    private void OnDisable()
    {
        health.Damaged -= OnDamaged;
        health.Healed -= OnHealed;
        health.Died -= OnDied;
        health.Blocked -= OnBlocked;
        spriteRenderer.color = baseColor;
    }

    private void Update()
    {
        Color color = Time.time < flashUntil ? flashColor : baseColor;

        if (health.IsInvulnerable && !health.IsDead && Mathf.Repeat(Time.time, blinkInterval * 2f) < blinkInterval)
            color.a *= blinkAlpha;

        spriteRenderer.color = color;
    }

    private void Flash(Color color)
    {
        flashColor = color;
        flashUntil = Time.time + flashDuration;
    }

    private void OnDamaged()
    {
        Flash(hitColor);
        FeedbackPlayer.Play(hitEffect, hitSound, Center);
        FeedbackPlayer.PlaySound(impactSound);
    }

    private void OnBlocked()
    {
        FeedbackPlayer.PlaySound(blockedSound);
    }

    private void OnHealed()
    {
        Flash(healColor);
        FeedbackPlayer.Play(healEffect, healSound, Center);
    }

    private void OnDied()
    {
        FeedbackPlayer.Play(deathEffect, deathSound, Center);
    }
}
