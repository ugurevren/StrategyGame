using UnityEngine;

namespace GridSystem
{
    public class PathNode
    {
        private Grid<PathNode> grid;
        public int x;
        public int y;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool isWalkable;
        public PathNode prevNode;
    
        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            isWalkable = true;
        }
    
        public void ChangeWalkable(bool isWalkable)
        {
            this.isWalkable = isWalkable;
        }

        public void CalculateFCost() {
            fCost = gCost + hCost;
        }

        public override string ToString()
        {
            return x + "," + y;
        }
    
        public Vector3 GetWorldPosition () {
            return grid.GetWorldPosition(x, y);
        }
    }
}
