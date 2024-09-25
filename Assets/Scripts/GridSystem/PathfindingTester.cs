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
    }
}
