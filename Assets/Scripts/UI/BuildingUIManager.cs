using System.Collections;
using System.Collections.Generic;
using GridSystem;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingUIManager : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on " + gameObject.name);
        var gridTester = GetComponentInParent<ReferenceHolder>().gridTester;
        gridTester.SelectUnit(gameObject.name.Substring(0,gameObject.name.IndexOf("(")));
    }
}
