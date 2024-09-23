using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class SoldierFactory : IUnitFactory
{
    private GameObject _soldierPrefab;

    public SoldierFactory(GameObject soldierPrefab)
    {
        _soldierPrefab = soldierPrefab;
    }

    public Unit CreateUnit(int soldierType, Vector2Int gridPos,List<UnitData> unitDataList)
    {
        var placedObjectWorldPosition = GridTester.Instance.GetGrid().GetWorldPosition(gridPos.x, gridPos.y);
        var soldier = PlacedObjectPool.Instance.Get(placedObjectWorldPosition, gridPos, _soldierPrefab.GetComponent<Poolable>()).GetComponent<Unit>();

        switch (soldierType)
        {
            case 1:
                soldier.SetUnitType(unitDataList[0]);
                break;
            case 2:
                soldier.SetUnitType(unitDataList[1]);
                break;
            case 3:
                soldier.SetUnitType(unitDataList[2]);
                break;
            default:
                return null;
        }

        return soldier;
    }
}

