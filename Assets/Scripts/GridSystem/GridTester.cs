using System.Collections.Generic;
using Building;
using Helpers;
using UnityEngine;

namespace GridSystem
{
    public class GridTester : MonoBehaviour
    {
        [SerializeField] private List<UnitSO> _buildingList;
        public UnitSO selectedUnit;
        
        [SerializeField] public int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _cellSize;
        [SerializeField] private Vector3 _originPosition;
        private static Grid<GridObject> _grid;
        [SerializeField] private IsPointerOverUI _isPointerOverUI;
        private bool _buildingMode = false;
        [SerializeField] private InfoPanel _infoPanel;

        private void Awake()
        {
            _grid = new Grid<GridObject>(_width, _height, _cellSize, _originPosition, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
            selectedUnit = _buildingList[0];
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isPointerOverUI.IsPointerOverUIElement()&&_buildingMode) 
            {
                if (_grid == null || selectedUnit == null)
                {
                    Debug.LogError("Grid or Selected Building is null");
                    return;
                }

                var mousePos= GetMouseWorldPosition();
                _grid.GetXY(mousePos, out var x, out var y);
                var gridObject = _grid.GetGridObject(x, y);

                if (gridObject == null)
                {
                    Debug.LogError("Grid Object is null");
                    return;
                }

                var gridPositionList = selectedUnit.GetGridPositionList(new Vector2Int(x, y));
                var canBuild = true;

                for(int i = 0; i<gridPositionList.Count; i++)
                {
                    var gridPosition = gridPositionList[i];
                    if (IsBuildable(gridPosition))
                    {
                        canBuild = true;
                    }
                    else
                    {
                        canBuild = false;
                        break;
                    }
                }
                
                if (canBuild)
                {
                    var placedObjectWorldPosition = _grid.GetWorldPosition(x, y);
                    var placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, y), selectedUnit,_infoPanel, _grid, this);
                    for(int i = 0; i<gridPositionList.Count; i++)
                    {
                        var gridPosition = gridPositionList[i];
                        SetGridType(_grid.GetWorldPosition(gridPosition.x, gridPosition.y), GridObject.GridType.Building, i, placedObject, selectedUnit);
                    }
                }
                else
                {
                    Debug.Log("Can't build here");
                }
                
            }
        }

        public bool IsBuildable( Vector2Int gridPosition)
        {
            return _grid.GetGridObject(gridPosition.x, gridPosition.y)?.Type == GridObject.GridType.Empty;
        }

        public void SelectUnit(string name)
        {
            for(int i = 0; i<_buildingList.Count; i++)
            {
                if (_buildingList[i].name != name) continue;
                selectedUnit = _buildingList[i];
                break;
            }
        }
        
        public Vector3 GetMouseWorldPosition()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
        public void SetGridType(Vector3 worldPosition, GridObject.GridType type, int index, UnitSO unitSo = null)
        {
            _grid.GetGridObject(worldPosition).Set(type, index, unitSo);
        }
        public void SetGridType(Vector3 worldPosition, GridObject.GridType type, int index, PlacedObject placedObject, UnitSO unitSo = null)
        {
            _grid.GetGridObject(worldPosition).Set(type, index, unitSo);
        }
        public bool GetProductionMode()
        {
            return _infoPanel.gameObject.activeSelf;
        }
        public Grid<GridObject> GetGrid()
        {
            return _grid;
        }
        
        public void BuildMode()
        {
            _buildingMode = !_buildingMode;
        }
        
        public bool GetBuildingMode()
        {
            return _buildingMode;
        }

        public void Clear()
        {
            if(_grid == null) return;
            // Clear the grid
            for (var x = 0; x < _grid.GetWidth(); x++)
            {
                for (var y = 0; y < _grid.GetHeight(); y++)
                {
                    _grid.GetGridObject(x, y).Set(GridObject.GridType.Empty, 0);
                }
            }
        }
       
    }
    
   
    
}
