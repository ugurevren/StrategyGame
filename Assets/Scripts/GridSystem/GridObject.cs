using _Poolable;

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
        public Poolable poolable;
        
        public GridObject(Grid<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.X = x;
            this.Y = y;
            this.Type = GridType.Empty;
        }
        
        public void Set(GridType type)
        {
            this.Type = type;
            grid.TriggerGridObjectChanged(X,Y);
        }
        public void Set(GridType type,Poolable poolable)
        {
            this.Type = type;
           this.poolable = poolable;
            grid.TriggerGridObjectChanged(X,Y);
        }
        public Poolable GetPoolable()
        {
            return poolable;
        }
        
        public override string ToString()
        {
            return ".";
        }
        
    }
}
