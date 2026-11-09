using UnityEngine;

[RequireComponent(typeof(Health))]
public class LootDropper : MonoBehaviour
{
    [System.Serializable]
    private struct LootEntry
    {
        public Pickup prefab;
        [Range(0f, 1f)] public float chance;
        [Min(1)] public int maxAmount;
    }

    [SerializeField] private LootEntry[] loot;
    [SerializeField] private Vector2 launchVelocity = new Vector2(2f, 5f);
    [SerializeField] private float spawnHeight = 0.5f;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.Died += Drop;
    }

    private void OnDisable()
    {
        health.Died -= Drop;
    }

    private void Drop()
    {
        Vector3 position = transform.position + Vector3.up * spawnHeight;

        foreach (LootEntry entry in loot)
        {
            if (entry.prefab == null || Random.value > entry.chance)
                continue;

            int amount = Random.Range(1, Mathf.Max(1, entry.maxAmount) + 1);

            for (int i = 0; i < amount; i++)
            {
                Pickup pickup = PoolRegistry.Get(entry.prefab, position);

                if (pickup != null)
                    pickup.Launch(new Vector2(Random.Range(-launchVelocity.x, launchVelocity.x), launchVelocity.y));
            }
        }
    }
}
