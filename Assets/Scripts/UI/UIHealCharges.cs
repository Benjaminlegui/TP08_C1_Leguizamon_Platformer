using UnityEngine;
using UnityEngine.UI;

public class UIHealCharges : MonoBehaviour
{
    [SerializeField] private HealState healState;
    [SerializeField] private Image[] slots;
    [Range(0f, 1f)]
    [SerializeField] private float emptyAlpha = 0.25f;

    private void OnEnable()
    {
        healState.ChargesChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        healState.ChargesChanged -= Refresh;
    }

    private void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Color color = slots[i].color;
            color.a = i < healState.Charges ? 1f : emptyAlpha;
            slots[i].color = color;
        }
    }
}
