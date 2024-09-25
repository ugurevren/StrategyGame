using System.Collections.Generic;
using _Poolable.Units;
using UI;
using UnityEngine;

namespace Helpers
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _soldierPrefab;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<UnitData> _soldierDataList;
        [SerializeField] private List<UnitData> _enemyDataList;
        [SerializeField] private InputController _inputController;
    
        public void SpawnSoldier(int unitTypeIndex, Vector2Int gridPos)
        {
            var soldierFactory = new UnitFactory(_soldierPrefab, UnitFactory.UnitType.Soldier);
            var soldier = soldierFactory.CreateUnit(gridPos, unitTypeIndex, null, _soldierDataList);
            _inputController.selectedSoldier = soldier.gameObject;
            InfoPanel.Instance.ClosePanel();
        }

        public void SpawnEnemy(int unitTypeIndex, Vector2Int gridPos)
        {
            var enemyFactory = new UnitFactory(_enemyPrefab, UnitFactory.UnitType.Enemy);
            var enemy = enemyFactory.CreateUnit(gridPos, unitTypeIndex, _enemyDataList);
        }
    }
}