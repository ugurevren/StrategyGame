using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Pathfinding
    {
        private static Pathfinding _instance;
        //Singleton
        public static Pathfinding Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Pathfinding();
                }
                return _instance;
                
            }
            private set => _instance = value;
        }

        private const int MOVE_STRAIGHT = 10;
        private const int MOVE_DIAGONAL = 14;

        private Grid<PathNode> _grid;
        private Grid<GridObject> _buildingGrid;
    
        private List<PathNode> _openList;
        private List<PathNode> _closedList;

        private Pathfinding()
        {
            
        }

        public Grid<PathNode> GetGrid() {
            return _grid;
        }
    
   
        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            var startNode = _grid.GetGridObject(startX, startY);
            var endNode = _grid.GetGridObject(endX, endY);

            _openList = new List<PathNode> { startNode };
            _closedList = new List<PathNode>();

            for (int x = 0; x < _grid.GetWidth(); x++) {
                for (int y = 0; y < _grid.GetHeight(); y++) {
                    var pathNode = _grid.GetGridObject(x, y);
                    pathNode.gCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.prevNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (_openList.Count > 0) {
                var currentNode = GetLowestFCostNode(_openList);
                if (currentNode == endNode) {
                    return CalculatePath(endNode);
                }

                _openList.Remove(currentNode);
                _closedList.Add(currentNode);

                foreach (PathNode neighborNode in GetNeighborList(currentNode)) {
                    if (_closedList.Contains(neighborNode)) continue;
                    neighborNode.ChangeWalkable(_buildingGrid.GetGridObject(neighborNode.x, neighborNode.y).Type == GridObject.GridType.Empty||
                                                _buildingGrid.GetGridObject(neighborNode.x, neighborNode.y).Type == GridObject.GridType.Enemy);
                    if (!neighborNode.isWalkable) {
                        _closedList.Add(neighborNode);
                        continue;
                    }

                    var tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighborNode);
                    if (tentativeGCost < neighborNode.gCost) {
                        neighborNode.prevNode = currentNode;
                        neighborNode.gCost = tentativeGCost;
                        neighborNode.hCost = CalculateDistanceCost(neighborNode, endNode);
                        neighborNode.CalculateFCost();

                        if (!_openList.Contains(neighborNode)) {
                            _openList.Add(neighborNode);
                        }
                    }
                }
            }
            return null;
        }

        private List<PathNode> GetNeighborList(PathNode currentNode) {
            List<PathNode> neighborList = new List<PathNode>();
        
            if (currentNode.x - 1 >= 0) {
                neighborList.Add(GetNode(currentNode.x - 1, currentNode.y)); //Left
                if (currentNode.y - 1 >= 0) neighborList.Add(GetNode(currentNode.x - 1, currentNode.y - 1)); //Left Down
                if (currentNode.y + 1 < _grid.GetHeight()) neighborList.Add(GetNode(currentNode.x - 1, currentNode.y + 1)); //Left Up
            }
            if (currentNode.x + 1 < _grid.GetWidth()) {
                neighborList.Add(GetNode(currentNode.x + 1, currentNode.y)); //Right
                if (currentNode.y - 1 >= 0) neighborList.Add(GetNode(currentNode.x + 1, currentNode.y - 1)); //Right Down
                if (currentNode.y + 1 < _grid.GetHeight()) neighborList.Add(GetNode(currentNode.x + 1, currentNode.y + 1)); //Right Up
            }
            if (currentNode.y - 1 >= 0) neighborList.Add(GetNode(currentNode.x, currentNode.y - 1)); //Down
            if (currentNode.y + 1 < _grid.GetHeight()) neighborList.Add(GetNode(currentNode.x, currentNode.y + 1)); //Up

            return neighborList;
        }

        private PathNode GetNode(int x, int y) {
            return _grid.GetGridObject(x, y);
        }

        private List<PathNode> CalculatePath(PathNode endNode) {
            var path = new List<PathNode>();
            path.Add(endNode);
            var currentNode = endNode;
            while (currentNode.prevNode != null) {
                path.Add(currentNode.prevNode);
                currentNode = currentNode.prevNode;
            }
            path.Reverse();
            return path;
        }

        private int CalculateDistanceCost(PathNode a, PathNode b) {
            var xDistance = Mathf.Abs(a.x - b.x);
            var yDistance = Mathf.Abs(a.y - b.y);
            var remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList) {
            var lowestFCostNode = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++) {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost) {
                    lowestFCostNode = pathNodeList[i];
                }
            }
            return lowestFCostNode;
        }
        public void SetPathfinding(int width, int height, Grid<GridObject> buildingGrid)
        {
            _grid = new Grid<PathNode>(width, height, 4f, new Vector3(-75,-50,0), (Grid<PathNode> global, int x, int y) => new PathNode(global, x, y));
            _buildingGrid = buildingGrid;
        }
    }
}