using System.Collections.Generic;
using UnityEngine;

public class PathfindingTester : MonoBehaviour
{
    private Pathfinding _pathfinding;
    void Start()
    {
        _pathfinding = new Pathfinding(20, 20);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            _pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = _pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                   Debug.Log(path[i]);
                }
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}
