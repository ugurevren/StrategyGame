using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Building
{
    [CreateAssetMenu]
    public class UnitSO : ScriptableObject, IAttackable
    {
        public string name;
        public Transform prefab;
        public Transform visual;
        public int width;
        public int height;
        public int health;
        public int energyCost;
        
        public List<Vector2Int> GetGridPositionList(Vector2Int offset)
        {
            var gridPositionList = new List<Vector2Int>();
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridPositionList.Add(new Vector2Int(x + offset.x, y + offset.y));
                }
            }
            return gridPositionList;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(this);
            }
        }
    }
}
