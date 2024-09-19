using GridSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class BuildingUIManager : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            GridTester.Instance.SelectUnit(gameObject.name.Substring(0,gameObject.name.IndexOf("(")));
        }
    }
}
