using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
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
        public Poolable poolable;
        public UnitSO UnitSo;
        
        public GridObject(Grid<GridObject> grid, int x, int y, int rotation = 0)
        {
            this.grid = grid;
            this.X = x;
            this.Y = y;
            this.Type = GridType.Empty;
            this.rotation = rotation;
        }
        
        public void Set(GridType type, int index, UnitSO unitSo = null)
        {
            this.Type = type;
            this.index = index;
            this.UnitSo = unitSo;
            grid.TriggerGridObjectChanged(X,Y);
        }
        public void Set(GridType type, int index,Poolable poolable)
        {
            this.Type = type;
            this.index = index;
           this.poolable = poolable;
            grid.TriggerGridObjectChanged(X,Y);
        }
        
        
        public List<Vector2Int> GetNotWalkableAreas(Grid<GridObject> grid)
        {
            var notWalkableAreas = new List<Vector2Int>();

            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    var gridObject = grid.GetGridObject(x, y);
                    if (gridObject.Type != GridType.Empty)
                    {
                        notWalkableAreas.Add(new Vector2Int(x, y));
                    }
                }
            }

            return notWalkableAreas;
        }
        
        public bool CheckIfPositionIsWalkable(GridObject gridObject)
        {
            return gridObject.Type == GridType.Empty;
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
