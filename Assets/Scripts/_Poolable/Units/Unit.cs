using Interfaces;
using UnityEngine;

namespace _Poolable.Units
{
    public class Unit : Poolable, IAttackable
    {
        public UnitData unitData;
        private int _health;
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        public void SetUnit(UnitData data)
        {
            unitData = data;
            _health = unitData.health;
            _spriteRenderer.sprite = unitData.sprite;
        }

        public bool TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                return true;
            }

            return false;
        }
    }
}