using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GridTester.Instance.GetMouseWorldPosition();
            Grid<GridObject>.Instance.GetXY(mouseWorldPosition, out int x, out int y);
            var type = Grid<GridObject>.Instance.GetGridObject(x, y).Type;
            switch (type)
            {
                case GridObject.GridType.Empty:
                    Debug.Log("empty");
                    break;
                case GridObject.GridType.Building:
                    Debug.Log("building");
                    break;
                case GridObject.GridType.Enemy:
                    Debug.Log("enemy");
                    break;
                case GridObject.GridType.FriendlyUnit:
                    //TODO soldier gride eklendi ancak hareketine göre grid type güncellenmeli
                    Debug.Log("friendly unit");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
