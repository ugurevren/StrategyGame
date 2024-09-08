using System;
using System.Collections.Generic;
using Building;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem
{
    public class GridTester : MonoBehaviour
    {
        [SerializeField] private List<UnitSO> _buildingList;
        [FormerlySerializedAs("selectedBuilding")] public UnitSO selectedUnit;
        
        [SerializeField] public int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _cellSize;
        [SerializeField] private Vector3 _originPosition;
        private Grid<GridObject> _grid;

        private void Awake()
        {
            _grid = new Grid<GridObject>(_width, _height, _cellSize, _originPosition, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
            selectedUnit = _buildingList[0];
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
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
                    if (_grid.GetGridObject(gridPosition.x, gridPosition.y)?.Type != GridObject.GridType.Empty)
                    {
                        canBuild = false;
                        break;
                    }
                }
                
                if (canBuild)
                {
                    var placedObjectWorldPosition = _grid.GetWorldPosition(x, y);
                    var placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, y), selectedUnit);
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

        public Vector3 GetMouseWorldPosition()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
        public void SetGridType(Vector3 worldPosition, GridObject.GridType type, int index, PlacedObject placedObject = null, UnitSO unitSo = null)
        {
            _grid.GetGridObject(worldPosition).Set(type, index, placedObject, unitSo);
        }
        
        public GridObject GetGridObject (int x, int y)
        {
            return _grid.GetGridObject(x, y);
        }
        
        public GridObject GetGridObject (Vector3 worldPosition)
        {
            return _grid.GetGridObject(worldPosition);
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
    
    public class GridObject
    {
        [System.Serializable]
        public enum GridType
        {
            Building,
            Enemy,
            FriendlyUnit,
            Empty
            
        }
        public Grid<GridObject> grid;
        public GridType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int index;
        public int rotation;
        public PlacedObject PlacedObject;
        public UnitSO UnitSo;
        
        public GridObject(Grid<GridObject> grid, int x, int y, int rotation = 0)
        {
            this.grid = grid;
            this.X = x;
            this.Y = y;
            this.Type = GridType.Empty;
            this.rotation = rotation;
        }
        
        public void Set(GridType type, int index, PlacedObject placedObject = null, UnitSO unitSo = null)
        {
            if (placedObject == null || unitSo == null)
            {
                Debug.LogError("PlacedObject or BuildingSO is null");
                return;
            }
            this.Type = type;
            this.index = index;
            this.PlacedObject = placedObject;
            this.UnitSo = unitSo;
            grid.TriggerGridObjectChanged(X,Y);
        }
        
        public override string ToString()
        {
            return ".";
        }
        
        public Vector3 GetWorldPosition()
        {
            return grid.GetWorldPosition(X, Y);
        }
    }
    
}
