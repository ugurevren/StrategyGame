using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class PlacedObjectPool : MonoBehaviour
{
    public static PlacedObjectPool Instance { get; private set; }

    private Queue<PlacedObject> pool = new Queue<PlacedObject>();

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

    public PlacedObject Get(Vector3 worldPosition, Vector2Int origin, UnitSO unitSo)
    {
        PlacedObject placedObject;
        if (pool.Count == 0)
        {
            placedObject = PlacedObject.Create(worldPosition, origin, unitSo);
        }
        else
        {
            placedObject = pool.Dequeue();
            placedObject.Reinitialize(worldPosition, origin, unitSo);
        }
        return placedObject;
    }

    public void ReturnToPool(PlacedObject placedObject)
    {
        pool.Enqueue(placedObject);
    }
}
