using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Image fill;

    private Health health;

    private void Awake()
    {
        health = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        health.Changed += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        health.Changed -= Refresh;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    private void Refresh()
    {
        if (health.MaxHealth <= 0)
            return;

        fill.fillAmount = (float)health.CurrentHealth / health.MaxHealth;
    }
}
