using GridSystem;
using UnityEngine;

namespace Helpers
{
    public class EnemySpawner : MonoBehaviour
    {
        private UnitSpawner _unitSpawner;
        [SerializeField] private int _x;
        [SerializeField] private int _y;

        private void Start()
        {
            _unitSpawner = GetComponent<UnitSpawner>();
            //InvokeRepeating("SpawnEnemy", 0, 15);
            SpawnEnemy();
        }

        public void SpawnEnemy()
        {
            _unitSpawner.SpawnEnemy(1,new Vector2Int(_x,_y));
        }
    }
}
