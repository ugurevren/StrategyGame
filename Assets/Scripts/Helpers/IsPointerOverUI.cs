using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helpers
{
    public class IsPointerOverUI : MonoBehaviour
    {
        // Singleton
        public static IsPointerOverUI Instance { get; private set; }
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

        private const int _uiLayer = 5;

        public bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
    
        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                var curRaycastResult = eventSystemRaysastResults[index];
                if (curRaycastResult.gameObject.layer == _uiLayer)
                    return true;
            }
            return false;
        }
    
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
    }
}
