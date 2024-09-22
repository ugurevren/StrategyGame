using System.Collections.Generic;
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
        var soldier = Object.Instantiate(_soldierPrefab).GetComponent<Unit>();
        soldier.Initialize(gridPos);

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

