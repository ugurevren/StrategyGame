using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class Barrack : BuildingBase
{
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!GridTester.Instance.GetBuildingMode())
        {
            Debug.Log("Barrack");
            if (CheckClick(3, 3))
            {
                Debug.Log("clicked");
                InfoPanel.Instance.OpenCloseInfoPanel();
                if (!InfoPanel.Instance.gameObject.activeSelf) return;
                InfoPanel.Instance.Clear();
                InfoPanel.Instance.CreateProductionUIItem(1);
                InfoPanel.Instance.CreateProductionUIItem(2);
                InfoPanel.Instance.CreateProductionUIItem(3);
            }
        }
    }

    public void SpawnUnit(int index)
    {
        //var unit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
        //unit.GetComponent<Unit>().SetUnitSO(index);
    }
}
