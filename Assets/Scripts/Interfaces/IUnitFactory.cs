using System.Collections.Generic;
using _Poolable.Units;
using UnityEngine;

namespace Interfaces
{
    public interface IUnitFactory
    {
        public Unit CreateUnit(Vector2Int gridPos,int unitType, List<UnitData> enemyDataList = null, List<UnitData> soldierDataList = null);
    }
}
