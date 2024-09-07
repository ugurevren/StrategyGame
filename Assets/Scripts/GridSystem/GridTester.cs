using UnityEngine;

namespace GridSystem
{
    public class GridTester : MonoBehaviour
    {
        [SerializeField] public int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _cellSize;
        [SerializeField] private Vector3 _originPosition;
        private Grid<GridObject> _grid;
    
        private void Start()
        {
            _grid = new Grid<GridObject>(_width, _height, _cellSize, _originPosition,
                (Grid<GridObject> g,int x, int y) => new GridObject(g,x,y),true);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GridObject gridObject = _grid.GetGridObject(GetMouseWorldPosition());
                if (gridObject != null)
                {
                    Debug.Log("Mouse position: " + GetMouseWorldPosition() + " GridObject: " + gridObject.X + "," + gridObject.Y);
                }
            }
        }
        public Vector3 GetMouseWorldPosition()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
        public void SetGridType(Vector3 worldPosition, GridObject.GridType type, int index)
        {
            _grid.GetGridObject(worldPosition).Set(type, index);
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
        
        public GridObject(Grid<GridObject> grid, int x, int y, int rotation = 0)
        {
            this.grid = grid;
            this.X = x;
            this.Y = y;
            this.Type = GridType.Empty;
            this.rotation = rotation;
        }
        
        public void Set(GridType type, int index)
        {
            this.Type = type;
            this.index = index;
            grid.TriggerGridObjectChanged(X,Y);
        }
        
        public override string ToString()
        {
            return Type.ToString()+index;
        }
        
        public Vector3 GetWorldPosition()
        {
            return grid.GetWorldPosition(X, Y);
        }
    }
    
}
