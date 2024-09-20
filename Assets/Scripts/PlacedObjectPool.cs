using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class PlacedObjectPool : MonoBehaviour
{
    public static PlacedObjectPool Instance { get; private set; }

    private Queue<Poolable> pool = new Queue<Poolable>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Poolable Get(Vector3 worldPosition, Vector2Int origin, Poolable obj)
    {
        Poolable poolable;
        if (pool.Count == 0)
        {
            poolable = Poolable.Create(worldPosition, origin, obj);
        }
        else
        {
            poolable = pool.Dequeue();
            poolable.Reinitialize(worldPosition, origin);
        }
        return poolable;
    }

    public void ReturnToPool(Poolable poolable)
    {
        pool.Enqueue(poolable);
    }
}
