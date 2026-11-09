using System.Collections.Generic;
using UnityEngine;

public class PoolRegistry : MonoBehaviour
{
    [SerializeField, Min(1)] private int initialSize = 5;

    private static PoolRegistry instance;
    private readonly Dictionary<Component, object> pools = new Dictionary<Component, object>();

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    public static T Get<T>(T prefab, Vector3 position) where T : Component
    {
        if (prefab == null)
            return null;

        if (instance == null)
        {
            Debug.LogWarning("PoolRegistry missing in scene, can't spawn " + prefab.name);
            return null;
        }

        return instance.GetPool(prefab).Get(position, Quaternion.identity);
    }

    private ObjectPool<T> GetPool<T>(T prefab) where T : Component
    {
        if (!pools.TryGetValue(prefab, out object pool))
        {
            pool = new ObjectPool<T>(prefab, initialSize, transform);
            pools.Add(prefab, pool);
        }

        return (ObjectPool<T>)pool;
    }
}
