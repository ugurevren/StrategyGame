using GridSystem;
using UnityEngine;

namespace Soldier
{
    public class SoldierSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _soldierPrefab;
    
        [SerializeField] private Sprite _soldier1Sprite;
        [SerializeField] private int _soldier1Damage;
    
        [SerializeField] private Sprite _soldier2Sprite;
        [SerializeField] private int _soldier2Damage;
    
        [SerializeField] private Sprite _soldier3Sprite;
        [SerializeField] private int _soldier3Damage;
    
        [SerializeField] GridTester _gridTester;
    
        public void SpawnSoldier(int soldierType, Vector2Int gridPos)
        {
            if (!GridTester.IsBuildable(gridPos)) return;
        
            var spawnPos = Grid<GridObject>.Instance.GetWorldPosition(gridPos.x, gridPos.y);
            var soldierGO = Instantiate(_soldierPrefab, spawnPos, Quaternion.identity);
            var soldierScript = soldierGO.GetComponent<Soldier>();
            soldierScript.SetOrigin(gridPos);

            switch (soldierType)
            {
                case 1:
                    soldierScript.SetSoldierType(_soldier1Damage,_soldier1Sprite);
                    break;
                case 2:
                    soldierScript.SetSoldierType(_soldier2Damage,_soldier2Sprite);
                    break;
                case 3:
                    soldierScript.SetSoldierType(_soldier3Damage,_soldier3Sprite);
                    break;
            }
        }
    
    }
}
