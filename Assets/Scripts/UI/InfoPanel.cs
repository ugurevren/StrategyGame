using System;
using System.Collections;
using System.Collections.Generic;
using Soldier;
using UnityEngine;

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
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    [SerializeField] private GameObject _productionUIItemPrefab;
    [SerializeField] private Transform _productionPanel;
    [SerializeField] private SoldierSpawner _soldierSpawner;
    
    public Sprite _soldier1Sprite;
    public int _soldier1Damage;
    public int _soldier1Cost;
    
    public Sprite _soldier2Sprite;
    public int _soldier2Damage;
    public int _soldier2Cost;
    
    public Sprite _soldier3Sprite;
    public int _soldier3Damage;
    public int _soldier3Cost;
    
    public void OpenCloseInfoPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void CreateProductionUIItem(int soldierType)
    {
        var productionUIItemGO = Instantiate(_productionUIItemPrefab, _productionPanel);
        var productionUIItemScript = productionUIItemGO.GetComponent<ProductionUIItem>();
        switch (soldierType)
        {
            case 1:
                productionUIItemScript.SetProductionUIItem(_soldier1Sprite, "Soldier1", _soldier1Cost,1, _soldierSpawner);
                break;
            case 2:
                productionUIItemScript.SetProductionUIItem(_soldier2Sprite, "Soldier2", _soldier2Cost,2, _soldierSpawner);
                break;
            case 3:
                productionUIItemScript.SetProductionUIItem(_soldier3Sprite, "Soldier3", _soldier3Cost,3, _soldierSpawner);
                break;
        }
    }

    public void Clear()
    {
        foreach (Transform child in _productionPanel)
        {
            Destroy(child.gameObject);
        }
    }
}
