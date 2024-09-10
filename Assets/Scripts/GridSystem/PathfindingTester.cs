using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class PathfindingTester : MonoBehaviour
    {
        private Pathfinding _pathfinding;
        [SerializeField] private GridTester _buildingGridTester;
        void Start()
        {
            _pathfinding = new Pathfinding(40, 40, _buildingGridTester.GetGrid());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                _pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
                List<PathNode> path = _pathfinding.FindPath(0, 0, x, y);
            }
        }
    
        public Pathfinding GetPathfinding()
        {
            return _pathfinding;
        }

        private Vector3 GetMouseWorldPosition()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
    }
}
