using System.Collections;
using System.Collections.Generic;
using Building;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, UnitSO unitSo)
    {
        var transform = Instantiate(unitSo.prefab, worldPosition, Quaternion.identity);
        var placedObject = transform.GetComponent<PlacedObject>();
        
        placedObject._unitSo = unitSo;
        placedObject._origin = origin;
        return placedObject;
    }

    private UnitSO _unitSo;
    private Vector2Int _origin;
    private List<Vector2Int> GetGridPositionList()
    {
        return _unitSo.GetGridPositionList(_origin);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
