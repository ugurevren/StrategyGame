using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class PathfindingTester : MonoBehaviour
    {
        
        [SerializeField] private GridTester _buildingGridTester;
        void Start()
        {
           Pathfinding.Instance.SetPathfinding(40,40, _buildingGridTester.GetGrid());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                //TODO camera zırtosu
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                Pathfinding.Instance.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
                List<PathNode> path = Pathfinding.Instance.FindPath(0, 0, x, y);
            }
        }
        private Vector3 GetMouseWorldPosition()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
    }
}
