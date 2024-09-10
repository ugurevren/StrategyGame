
using System.Collections.Generic;
using Building;
using GridSystem;
using UnityEngine;
public class PlacedObject : MonoBehaviour
{
    private InfoPanel _infoPanel;
    private static Grid<GridObject> _grid;
    private GridTester _gridTester;
    private bool _created = false;
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, UnitSO unitSo, InfoPanel infoPanel, Grid<GridObject> grid, GridTester gridTester)
    {
        Debug.Log("PlacedObject created");
        var transform = Instantiate(unitSo.prefab, worldPosition, Quaternion.identity);
        var placedObject = transform.GetComponent<PlacedObject>();

        _grid = grid;
        placedObject._unitSo = unitSo;
        placedObject._origin = origin;
        placedObject._infoPanel = infoPanel;
        placedObject._gridTester = gridTester;
        placedObject._created = true;
        return placedObject;
    }

    private UnitSO _unitSo;
    private Vector2Int _origin;
    private List<Vector2Int> GetGridPositionList()
    {
        return _unitSo.GetGridPositionList(_origin);
    }

    private void Update()
    {
        // check if clicked on this object
        if(!_created)return;
        if (Input.GetMouseButtonDown(0)&&!_gridTester.GetBuildingMode())
        {
            var worldPosition = GetMouseWorldPosition();
            _grid.GetXY(worldPosition, out var x, out var y);
            if (x >= _origin.x && x < _origin.x + _unitSo.width &&
                y >= _origin.y && y < _origin.y + _unitSo.height)
            {
                switch (_unitSo.name)
                {
                    case "Barrack":
                        _infoPanel.OpenCloseInfoPanel();
                        if (!_infoPanel.gameObject.activeSelf) return;
                        _infoPanel.Clear();
                        _infoPanel.CreateProductionUIItem(1);
                        _infoPanel.CreateProductionUIItem(2);
                        _infoPanel.CreateProductionUIItem(3);
                        break;
                
                }
            }
        }
        
    }
    public Vector3 GetMouseWorldPosition()
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}
 