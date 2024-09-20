using GridSystem;
using Interfaces;
using UnityEngine;

public class BuildingBase : Poolable,IAttackable
{
    public int width;
    public int height;

    protected bool CheckClick(int width, int height)
    {
        var worldPosition = GridTester.Instance.GetMouseWorldPosition();
        Grid<GridObject>.Instance.GetXY(worldPosition, out var x, out var y);
        if (x >= _origin.x && x < _origin.x + width &&
            y >= _origin.y && y < _origin.y + height)
        {
            return true;
            //switch (_unitSo.name)
            //{
            //    case "Barrack":
            //        InfoPanel.Instance.OpenCloseInfoPanel();
            //        if (!InfoPanel.Instance.gameObject.activeSelf) return;
            //        InfoPanel.Instance.Clear();
            //        InfoPanel.Instance.CreateProductionUIItem(1);
            //        InfoPanel.Instance.CreateProductionUIItem(2);
            //        InfoPanel.Instance.CreateProductionUIItem(3);
            //        break;
            //    
            //}
        }

        return false;
    }

    public bool TakeDamage(int damage, out bool isDead)
    {
        Debug.Log("Building Take Damage");
        return isDead = true;
    }
}
