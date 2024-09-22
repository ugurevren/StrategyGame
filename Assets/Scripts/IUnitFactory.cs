using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitFactory
{
    public Unit CreateUnit(int type, Vector2Int gridPos, List<UnitData> unitDataList);
}
