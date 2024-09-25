using System.Collections.Generic;
using _Poolable.Units;
using Helpers;
using UnityEngine;

namespace UI
{
    public class InfoPanel : MonoBehaviour
    {
        // Singleton
        public static InfoPanel Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    
        [SerializeField] private List<UnitData> _soldierDataList;
        private bool _isProductionUIItemCreated=false;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        [SerializeField] private GameObject _productionUIItemPrefab;
        [SerializeField] private Transform _productionPanel;
        [SerializeField] private UnitSpawner unitSpawner;
    
        public void OpenCloseInfoPanel()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        public void OpenPanel()
        {
            gameObject.SetActive(true);
        }
        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }
        public void CreateProductionUIItem()
        {
            if (_isProductionUIItemCreated) return;
            for(int i = 0; i<_soldierDataList.Count; i++)
            {
                var productionUIItemGO = Instantiate(_productionUIItemPrefab, _productionPanel);
                var productionUIItemScript = productionUIItemGO.GetComponent<ProductionUIItem>();
                productionUIItemScript.SetProductionUIItem(_soldierDataList[i].sprite, _soldierDataList[i].unitTypeIndex, unitSpawner);
            }
            _isProductionUIItemCreated = true;
        }
    }
}
