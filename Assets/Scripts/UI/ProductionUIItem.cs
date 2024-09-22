using System.Collections;
using GridSystem;
using Soldier;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductionUIItem : MonoBehaviour, IPointerClickHandler
{
    public Sprite sprite;
    public string itemName;
    public int itemCost;
    private Image _image;
    private SoldierSpawner _soldierSpawner;
    private int _soldierType;
    private Transform _selectedSpawnPoint;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
    }

    public void SetProductionUIItem(Sprite sprite, string itemName, int itemCost, int soldierType,SoldierSpawner soldierSpawner)
    {
        this.sprite = sprite;
        _image.sprite = sprite;
        this.itemName = itemName;
        this.itemCost = itemCost;
        _soldierType = soldierType;
        _soldierSpawner = soldierSpawner;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // wait for another Input
       StartCoroutine(WaitForSecondInput());
       
    }
    private IEnumerator WaitForSecondInput()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Wait for left mouse button click

        var worldPosition = GridTester.Instance.GetMouseWorldPosition();
        Grid<GridObject>.Instance.GetXY(worldPosition, out var x, out var y);
        if (Grid<GridObject>.Instance.GetGridObject(x,y).Type == GridObject.GridType.Empty)
        {
            _soldierSpawner.SpawnSoldier(_soldierType, new Vector2Int(x,y));
        }
    }
}
 