using GridSystem;
using Helpers;
using UnityEngine;

namespace Building
{
    public class BuildingGhost : MonoBehaviour
    {
        private Transform _visual;
        private UnitSO _unitSo;
        private Vector3 _targetPosition;
        [SerializeField] private GridTester _gridTester;
        [SerializeField] private IsPointerOverUI _isPointerOverUI;

        private void Start()
        {
            RefreshVisual();
        
        }
        private void LateUpdate()
        {
            if (_unitSo == null)
            {
                return;
            }

            if (!_gridTester.GetBuildingMode()||_isPointerOverUI.IsPointerOverUIElement())
            {
                if (_visual.gameObject.activeSelf)_visual.gameObject.SetActive(false);
                return;
            }
            if(!_visual.gameObject.activeSelf)_visual.gameObject.SetActive(true);
            RefreshVisual();
            var mousePos = _gridTester.GetGrid().GetMouseSnappedWorldPosition();
            _targetPosition = new Vector3(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y), 0);
            _visual.position = _targetPosition;
        
        }
    
        public Vector3 GetMouseWorldPosition()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
        private void RefreshVisual()
        {
            if (_unitSo != null)
            {
                Destroy(_visual.gameObject);
            }
            _unitSo = _gridTester.selectedUnit;
            if (_unitSo == null)
            {
                return;
            }
            _visual = Instantiate(_unitSo.visual, _targetPosition, Quaternion.identity);
        }
    }
}
