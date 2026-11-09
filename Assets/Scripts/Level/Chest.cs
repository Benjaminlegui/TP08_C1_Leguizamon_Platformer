using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Color openColor = new Color(1f, 0.85f, 0.2f);
    [SerializeField] private PooledEffect openEffect;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip lockedSound;

    private bool isOpen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (isOpen || player == null)
            return;

        if (!player.HasKey)
        {
            FeedbackPlayer.PlaySound(lockedSound);
            return;
        }

        Open(player);
    }

    private void Open(Player player)
    {
        isOpen = true;
        player.UseKey();

        if (openSprite != null)
            spriteRenderer.sprite = openSprite;
        else
            spriteRenderer.color = openColor;

        FeedbackPlayer.Play(openEffect, openSound, transform.position);
        gameManager.WinGame();
    }
}
