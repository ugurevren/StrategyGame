using GridSystem;
using UnityEngine;
public class PlacedObject : MonoBehaviour
{
    private bool _created = false;
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, UnitSO unitSo)
    {
        var transform = Instantiate(unitSo.prefab, worldPosition, Quaternion.identity);
        // TODO
        var placedObject = transform.GetComponent<PlacedObject>();
        
        placedObject._unitSo = unitSo;
        placedObject._origin = origin;
        placedObject._created = true;
        return placedObject;
    }

    private UnitSO _unitSo;
    private Vector2Int _origin;

    private void Update()
    {
        // check if clicked on this object
        if(!_created)return;
        if (Input.GetMouseButtonDown(0)&&!GridTester.Instance.GetBuildingMode())
        {
            var worldPosition = GridTester.Instance.GetMouseWorldPosition();
            Grid<GridObject>.Instance.GetXY(worldPosition, out var x, out var y);
            if (x >= _origin.x && x < _origin.x + _unitSo.width &&
                y >= _origin.y && y < _origin.y + _unitSo.height)
            {
                switch (_unitSo.name)
                {
                    case "Barrack":
                        InfoPanel.Instance.OpenCloseInfoPanel();
                        if (!InfoPanel.Instance.gameObject.activeSelf) return;
                        InfoPanel.Instance.Clear();
                        InfoPanel.Instance.CreateProductionUIItem(1);
                        InfoPanel.Instance.CreateProductionUIItem(2);
                        InfoPanel.Instance.CreateProductionUIItem(3);
                        break;
                
                }
            }
        }
        
    }
    public void Reinitialize(Vector3 worldPosition, Vector2Int origin, UnitSO unitSo)
    {
        // Reinitialize the object with new parameters
        transform.position = worldPosition;
        _unitSo = unitSo;
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
 