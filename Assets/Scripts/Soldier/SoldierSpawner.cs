using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace Soldier
{
    public class SoldierSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _soldierPrefab;
        [SerializeField] private List<UnitData> _unitDataList;
        
        public void SpawnSoldier(int soldierType, Vector2Int gridPos)
        {
            var soldierFactory = new SoldierFactory(_soldierPrefab);
            var soldier = soldierFactory.CreateUnit(soldierType, gridPos, _unitDataList);
        }
    
    }
}
