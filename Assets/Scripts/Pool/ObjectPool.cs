using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T prefab;
    private readonly Transform parent;
    private readonly Queue<T> available = new Queue<T>();
    private readonly HashSet<T> inUse = new HashSet<T>();

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            available.Enqueue(Create());
        }
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        T item = available.Count > 0 ? available.Dequeue() : Create();

        item.transform.SetPositionAndRotation(position, rotation);
        item.gameObject.SetActive(true);
        inUse.Add(item);

        if (item is IPoolable poolable)
            poolable.OnGet();

        return item;
    }

    public void Release(T item)
    {
        if (!inUse.Remove(item))
            return;

        if (item is IPoolable poolable)
            poolable.OnRelease();

        item.gameObject.SetActive(false);
        available.Enqueue(item);
    }

    public void ReleaseAll()
    {
        foreach (T item in new List<T>(inUse))
        {
            Release(item);
        }
    }

    private T Create()
    {
        T item = Object.Instantiate(prefab, parent);
        item.gameObject.SetActive(false);

        if (item is IPoolable poolable)
            poolable.OnCreated(() => Release(item));

        return item;
    }
}
