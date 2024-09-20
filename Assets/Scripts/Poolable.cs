using GridSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class Poolable : MonoBehaviour
{
    public GameObject prefab;
    protected bool _created = false;
    protected Vector2Int _origin;
    public Transform ghostVisual;
    public static Poolable Create(Vector3 worldPosition, Vector2Int origin, Poolable obj)
    {
        var transform = Instantiate(obj.prefab, worldPosition, Quaternion.identity);
        // TODO
        var placedObject = transform.GetComponent<Poolable>();
        
        placedObject._origin = origin;
        placedObject._created = true;
        return placedObject;
    }
    public void Reinitialize(Vector3 worldPosition, Vector2Int origin)
    {
        // Reinitialize the object with new parameters
        transform.position = worldPosition;
        _origin = origin;
        _created = true;
    }
    public Vector3 GetMouseWorldPosition()
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}
 