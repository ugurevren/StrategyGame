using _Poolable;
using GridSystem;
using UnityEngine;

namespace Helpers
{
    public class BuildingGhost : MonoBehaviour
    {
        //Singleton
        public static BuildingGhost Instance { get; private set; }
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
        private Vector3 _targetPosition = Vector3.zero;
        private GameObject _ghost;
        
        public void StartGhosting(Poolable poolable)
        {
            _ghost = Instantiate(poolable.prefab, _targetPosition, Quaternion.identity);
        }
        public void StopGhosting()
        {
            Destroy(_ghost);
        }
        private void LateUpdate()
        {
           
            if (_ghost == null) return;
            
            if (IsPointerOverUI.Instance.IsPointerOverUIElement())
            {
                if (_ghost.activeSelf)_ghost.SetActive(false);
                return;
            }
            if(!_ghost.activeSelf)_ghost.SetActive(true);
            
            var mousePos = GridTester.Instance.GetGrid().GetMouseSnappedWorldPosition();
            _targetPosition = new Vector3(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y), 0);
            _ghost.transform.position = _targetPosition;
        
        }
        
    }
}