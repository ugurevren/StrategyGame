using System.Collections;
using System.Collections.Generic;
using Building;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, BuildingSO buildingSo)
    {
        var transform = Instantiate(buildingSo.prefab, worldPosition, Quaternion.identity);
        var placedObject = transform.GetComponent<PlacedObject>();
        
        placedObject._buildingSo = buildingSo;
        placedObject._origin = origin;
        return placedObject;
    }

    private BuildingSO _buildingSo;
    private Vector2Int _origin;
    private List<Vector2Int> GetGridPositionList()
    {
        return _buildingSo.GetGridPositionList(_origin);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
