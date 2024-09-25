using System.Collections.Generic;
using _Poolable;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }

    private Dictionary<string, Queue<Poolable>> pools = new Dictionary<string, Queue<Poolable>>();

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

    public Poolable Get(string type, Vector3 worldPosition, Vector2Int origin, Poolable obj)
    {
        if (!pools.ContainsKey(type))
        {
            pools[type] = new Queue<Poolable>();
        }

        Poolable poolable;
        if (pools[type].Count == 0)
        {
            poolable = Poolable.Create(worldPosition, origin, obj);
        }
        else
        {
            poolable = pools[type].Dequeue();
            poolable.Reinitialize(worldPosition, origin);
            poolable.gameObject.SetActive(true);
        }
        return poolable;
    }

    public void ReturnToPool(string type, Poolable poolable)
    {
        if (!pools.ContainsKey(type))
        {
            pools[type] = new Queue<Poolable>();
        }

        poolable.gameObject.SetActive(false);
        pools[type].Enqueue(poolable);
    }
}
