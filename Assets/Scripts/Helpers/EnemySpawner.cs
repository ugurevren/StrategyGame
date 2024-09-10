using Building;
using GridSystem;
using UnityEngine;

namespace Helpers
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GridTester _gridTester;
        [SerializeField] private UnitSO _unitSO;
        [SerializeField] private int _x;
        [SerializeField] private int _y;

        private void Start()
        {
            SpawnEnemy();
        }

        public void SpawnEnemy()
        {
            var gridPos = new Vector2Int(_x, _y);
            var spawnPos = Grid<GridObject>.Instance.GetWorldPosition(gridPos.x, gridPos.y);
            var go= Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            go.GetComponent<Enemy>().unitSo = _unitSO;
            go.GetComponent<Enemy>().unitSo.SetCreatedGameObject(go);
            _gridTester.SetGridType(spawnPos,GridObject.GridType.Enemy,0,go.GetComponent<Enemy>().unitSo);
        }
    }
}
