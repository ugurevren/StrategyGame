using System.Collections.Generic;
using _Poolable;
using _Poolable.Buildings;
using Helpers;
using UnityEngine;

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
            }

            _grid = new Grid<GridObject>(_width, _height, _cellSize, _originPosition, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
            _camera = Camera.main;
        }

        public bool IsBuildable( Vector2Int gridPosition)
        {
            return _grid.GetGridObject(gridPosition.x, gridPosition.y)?.Type == GridObject.GridType.Empty;
        }
        public List<Vector2Int> GetGridPositionList(Vector2Int offset, int width, int height)
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
        public void SelectBuilding(string name)
        {
            for(int i = 0; i<_buildingList.Count; i++)
            {
                if (_buildingList[i].name != name) continue;
                selectedBuilding = _buildingList[i];
                BuildingGhost.Instance.StartGhosting(selectedBuilding);
                break;
            }
        }
        public Vector3 GetMouseWorldPosition()
        {
            var worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
        public void SetGridType(Vector3 worldPosition, GridObject.GridType type)
        {
            _grid.GetGridObject(worldPosition).Set(type);
        }
        public void SetGridType(Vector3 worldPosition, GridObject.GridType type, Poolable poolable)
        {
            _grid.GetGridObject(worldPosition).Set(type, poolable);
        }
        public Grid<GridObject> GetGrid()
        {
            return _grid;
        }
        
        public void ChangeBuildMode()
        {
            _buildingMode = !_buildingMode;
        }
        
        public bool GetBuildingMode()
        {
            return _buildingMode;
        }
        public Vector2Int GetGridOrigin()
        {
            return new Vector2Int((int)_originPosition.x, (int)_originPosition.y);
        }

        public void Clear()
        {
            if(_grid == null) return;
            // Clear the grid
            for (var x = 0; x < _grid.GetWidth(); x++)
            {
                for (var y = 0; y < _grid.GetHeight(); y++)
                {
                    _grid.GetGridObject(x, y).Set(GridObject.GridType.Empty);
                }
            }
        }
       
    }
    
   
    
}
