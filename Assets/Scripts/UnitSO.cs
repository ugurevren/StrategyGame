using System.Collections.Generic;
using GridSystem;
using Interfaces;
using UnityEngine;

[CreateAssetMenu]
public class UnitSO : ScriptableObject, IAttackable
{
    public string name;
    public Transform prefab;
    public GameObject createdGameObject;
    public Transform visual;
    public Sprite sprite;
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
    public bool TakeDamage(int damage, out bool isDead)
    {
        health -= damage;
        if (health <= 0)
        {
            var worldPos = Grid<GridObject>.Instance.GetWorldPosition((int) prefab.position.x, (int) prefab.position.z);
            Grid<GridObject>.Instance.GetGridObject(worldPos).Set(GridObject.GridType.Empty, 0);
            isDead = true;
            return true;
        }
        isDead = false;
        return false;
    }
    public void SetCreatedGameObject(GameObject go)
    {
        createdGameObject = go;
    }
    public GameObject GetCreatedGameObject()
    {
        return createdGameObject;
    }
}