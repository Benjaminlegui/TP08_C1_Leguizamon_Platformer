using UnityEngine;
using UnityEngine.UI;

public class UIKeyIndicator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image slot;
    [Range(0f, 1f)]
    [SerializeField] private float emptyAlpha = 0.25f;

    private void OnEnable()
    {
        player.KeyChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        player.KeyChanged -= Refresh;
    }

    private void Refresh()
    {
        Color color = slot.color;
        color.a = player.HasKey ? 1f : emptyAlpha;
        slot.color = color;
    }
}
