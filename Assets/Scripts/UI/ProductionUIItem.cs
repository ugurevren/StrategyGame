using System.Collections;
using GridSystem;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ProductionUIItem : MonoBehaviour, IPointerClickHandler
    {
        private Image _image;
        private UnitSpawner _unitSpawner;
        private int _unitTypeIndex;
        private Transform _selectedSpawnPoint;

        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
        }

        public void SetProductionUIItem(Sprite sprite,int soldierType,UnitSpawner unitSpawner)
        {
            _image.sprite = sprite;
            _unitTypeIndex = soldierType;
            _unitSpawner = unitSpawner;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StartCoroutine(WaitForSecondInput());
        }
    
        private IEnumerator WaitForSecondInput()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Wait for left mouse button click

            var worldPosition = GridTester.Instance.GetMouseWorldPosition();
            Grid<GridObject>.Instance.GetXY(worldPosition, out var x, out var y);
            if (Grid<GridObject>.Instance.GetGridObject(x,y).Type == GridObject.GridType.Empty)
            {
                _unitSpawner.SpawnSoldier(_unitTypeIndex, new Vector2Int(x,y));
            }
        }
    }
}
 