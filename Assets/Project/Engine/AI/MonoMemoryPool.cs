using UnityEngine;
using Zenject;

public abstract class MonoMemoryPool<TParam1, TValue> : MemoryPool<TParam1, TValue>
    where TValue : Component
{
    Transform _originalParent;

    public Agent Agent
    {
        get => default;
        set
        {
        }
    }

    protected override void OnCreated(TValue item)
    {
       // item.gameObject.SetActive(false);
        // Record the original parent which will be set to whatever is used in the UnderTransform method
        _originalParent = item.transform.parent;
    }

    protected override void OnDestroyed(TValue item)
    {
        GameObject.Destroy(item.gameObject);
    }

    protected override void OnSpawned(TValue item)
    {
        item.gameObject.SetActive(false);
    }

    protected override void OnDespawned(TValue item)
    {
        item.gameObject.SetActive(false);

        if (item.transform.parent != _originalParent)
        {
            item.transform.SetParent(_originalParent, false);
        }
    }
}