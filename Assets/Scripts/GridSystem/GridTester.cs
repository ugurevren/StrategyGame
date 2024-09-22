using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem
{
    public class GridTester : MonoBehaviour
    {
        // Singleton
        public static GridTester Instance { get; private set; }
        
        [SerializeField] private List<BuildingBase> _buildingList;
        public BuildingBase selectedBuilding;
        
        [SerializeField] public int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _cellSize;
        [SerializeField] private Vector3 _originPosition;
        private static Grid<GridObject> _grid;
        private bool _buildingMode = false;
        [SerializeField] private InfoPanel _infoPanel;
        private Camera _camera;

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

            _grid = new Grid<GridObject>(_width, _height, _cellSize, _originPosition, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || IsPointerOverUI.Instance.IsPointerOverUIElement() ||
                !_buildingMode) return;
            
            var mousePos= GetMouseWorldPosition();
            _grid.GetXY(mousePos, out var x, out var y);
            var gridObject = _grid.GetGridObject(x, y);
            
            // Check if the grid object is null
            if (gridObject == null) return;
                
            // Unit may occupy more than one space so we need to check all of them
            Debug.Log(selectedBuilding);
            var gridPositionList = GetGridPositionList(new Vector2Int(x, y),selectedBuilding.width, selectedBuilding.height); 
                
            // Check if all grid positions are buildable
            if (gridPositionList.Any(gridPosition => !IsBuildable(gridPosition)))
            {
                return;
            }
                
            // Place the object
            var placedObjectWorldPosition = _grid.GetWorldPosition(x, y);
            Debug.Log(selectedBuilding);
            var placedObject = PlacedObjectPool.Instance.Get(placedObjectWorldPosition, new Vector2Int(x, y), selectedBuilding);
            for (int i = 0; i < gridPositionList.Count; i++)
            {
                var gridPosition = gridPositionList[i];
                SetGridType(_grid.GetWorldPosition(gridPosition.x, gridPosition.y),
                    GridObject.GridType.Building, i, placedObject);
            }
        }

        public bool IsBuildable( Vector2Int gridPosition)
        {
            return _grid.GetGridObject(gridPosition.x, gridPosition.y)?.Type == GridObject.GridType.Empty;
        }
        private List<Vector2Int> GetGridPositionList(Vector2Int offset, int width, int height)
        {
            var gridPositionList = new List<Vector2Int>();
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridPositionList.Add(new Vector2Int(x + offset.x, y + offset.y));
                }
            }
            return gridPositionList;
        }
        public void SelectUnit(string name)
        {
            Debug.Log(name);
            
            for(int i = 0; i<_buildingList.Count; i++)
            {
                Debug.Log(_buildingList[i].name);
                if (_buildingList[i].name != name) continue;
                selectedBuilding = _buildingList[i];
                break;
            }
        }
        public Vector3 GetMouseWorldPosition()
        {
            var worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
        public void SetGridType(Vector3 worldPosition, GridObject.GridType type, int index, UnitSO unitSo = null)
        {
            _grid.GetGridObject(worldPosition).Set(type, index, unitSo);
        }
        public void SetGridType(Vector3 worldPosition, GridObject.GridType type, int index, Poolable poolable)
        {
            _grid.GetGridObject(worldPosition).Set(type, index, poolable);
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
