using GridSystem;
using Interfaces;

namespace _Poolable.Buildings
{
    public class BuildingBase : Poolable,IAttackable
    {
        public int width;
        public int height;
        public int health;
    
        protected bool CheckClick()
        {
            var worldPosition = GridTester.Instance.GetMouseWorldPosition();
            Grid<GridObject>.Instance.GetXY(worldPosition, out var x, out var y);
            if (x >= _origin.x && x < _origin.x + width &&
                y >= _origin.y && y < _origin.y + height)
            {
                return true;
            }

            return false;
        }

        public bool TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
