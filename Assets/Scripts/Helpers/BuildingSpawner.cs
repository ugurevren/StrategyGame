using System.Linq;
using GridSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] private Button _buildingUIButton;
   
        // Singleton
        public static BuildingSpawner Instance { get; private set; }
  

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
        }
    
        public void SpawnBuilding()
        {
            var mousePos = GridTester.Instance.GetMouseWorldPosition();
            Grid<GridObject>.Instance.GetXY(mousePos, out var x, out var y);
        
            // Unit may occupy more than one space so we need to check all of them
            var gridPositionList = GridTester.Instance.GetGridPositionList(new Vector2Int(x, y),
                GridTester.Instance.selectedBuilding.width, GridTester.Instance.selectedBuilding.height);

            // Check if all grid positions are buildable
            if (gridPositionList.Any(gridPosition => !GridTester.Instance.IsBuildable(gridPosition)))
            {
                return;
            }

            // Place the object
            var placedObjectWorldPosition = GridTester.Instance.GetGrid().GetWorldPosition(x, y);
            var placedObject = Pool.Instance.Get("Building",placedObjectWorldPosition, new Vector2Int(x, y),
                GridTester.Instance.selectedBuilding);
            for (int i = 0; i < gridPositionList.Count; i++)
            {
                var gridPosition = gridPositionList[i];
                GridTester.Instance.SetGridType(GridTester.Instance.GetGrid().GetWorldPosition(gridPosition.x, gridPosition.y),
                    GridObject.GridType.Building, placedObject);
            }
            _buildingUIButton.onClick.Invoke();
        }
    }
}
