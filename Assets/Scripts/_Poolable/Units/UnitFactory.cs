using System.Collections.Generic;
using GridSystem;
using Interfaces;
using UnityEngine;

namespace _Poolable.Units
{
    public class UnitFactory : IUnitFactory
    {
        private readonly GameObject _unitPrefab;
        private readonly UnitType _unitType;
    
        public enum UnitType
        {
            Soldier,
            Enemy
        }
    
        public UnitFactory(GameObject soldierPrefab, UnitType unitType)
        {
            _unitPrefab = soldierPrefab;
            _unitType = unitType;
        }
    
        public Unit CreateUnit(Vector2Int gridPos,int unitType, List<UnitData> enemyDataList = null, List<UnitData> soldierDataList = null )
        {
            var placedObjectWorldPosition = GridTester.Instance.GetGrid().GetWorldPosition(gridPos.x, gridPos.y);
            switch (_unitType)
            {
                case UnitType.Enemy:
                    var enemy = Pool.Instance.Get("Enemy",placedObjectWorldPosition, gridPos, _unitPrefab.GetComponent<Poolable>()).GetComponent<Unit>();
                    GridTester.Instance.SetGridType(placedObjectWorldPosition, GridObject.GridType.Enemy, enemy);

                    switch (unitType)
                    {
                        case 1:
                            enemy.SetUnit(enemyDataList[0]);
                            break;
                        default:
                            return null;
                    }
                    return enemy;
                
                case UnitType.Soldier:
                    var soldier = Pool.Instance.Get("Soldier",placedObjectWorldPosition, gridPos, _unitPrefab.GetComponent<Poolable>()).GetComponent<Unit>();
                    GridTester.Instance.SetGridType(placedObjectWorldPosition, GridObject.GridType.FriendlyUnit, soldier);
                
                
                    switch (unitType)
                    {
                        case 1:
                            soldier.SetUnit(soldierDataList[0]);
                            break;
                        case 2:
                            soldier.SetUnit(soldierDataList[1]);
                            break;
                        case 3:
                            soldier.SetUnit(soldierDataList[2]);
                            break;
                        default:
                            return null;
                    }

                    return soldier;
            
                default:
                    return null;
            }
        }
    }
}
