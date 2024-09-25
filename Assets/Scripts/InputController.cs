using System;
using System.Collections;
using System.Collections.Generic;
using _Poolable.Units;
using GridSystem;
using Helpers;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject selectedSoldier;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!Helpers.IsPointerOverUI.Instance.IsPointerOverUIElement())
        {
            var mouseWorldPosition = GridTester.Instance.GetMouseWorldPosition();
            Grid<GridObject>.Instance.GetXY(mouseWorldPosition, out int x, out int y);
            var gridObject = Grid<GridObject>.Instance.GetGridObject(x, y);
            switch (gridObject.Type)
            {
                case GridObject.GridType.Empty:
                    if (GridTester.Instance.selectedBuilding != null&& GridTester.Instance.GetBuildingMode())
                    {
                        BuildingSpawner.Instance.SpawnBuilding();
                        BuildingGhost.Instance.StopGhosting();
                    }
                    break;
                case GridObject.GridType.Building:;
                    var building = gridObject.GetPoolable();
                    building.HandleClick();
                    break;
                case GridObject.GridType.Enemy:
                    // Implement enemy click if needed
                    break;
                case GridObject.GridType.FriendlyUnit:
                    var soldier = gridObject.GetPoolable();
                    selectedSoldier = soldier.gameObject;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }else if (Input.GetMouseButtonDown(1)&&selectedSoldier!=null)
        {
            selectedSoldier.GetComponent<Soldier>().HandleRightClick();
        }
    }
}
