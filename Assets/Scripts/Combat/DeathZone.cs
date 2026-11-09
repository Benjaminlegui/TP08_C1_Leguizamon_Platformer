using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        KillPlayer(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        KillPlayer(collision.collider);
    }

    private void KillPlayer(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player != null)
            player.GetComponent<Health>().Kill();
    }
}
