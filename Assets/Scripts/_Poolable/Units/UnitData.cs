using UnityEngine;

namespace _Poolable.Units
{
    [CreateAssetMenu]
    public class UnitData : ScriptableObject
    {
        public Sprite sprite;
        public int damage;
        public int health;
        public int energyCost;
        public int unitTypeIndex;
        public enum UnitType
        {
            MeleeSoldier,
            Enemy
        }
        public UnitType unitType;
    }
}
