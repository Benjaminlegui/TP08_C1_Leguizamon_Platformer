using System;
using UnityEngine;

public abstract class Spawn : MonoBehaviour, IPoolable
{
    private Action release;

    void IPoolable.OnCreated(Action release)
    {
        this.release = release;
    }

    public virtual void OnGet()
    {
    }

    public virtual void OnRelease()
    {
    }

    public void Despawn()
    {
        if (release != null)
            release();
        else
            gameObject.SetActive(false);
    }
}
