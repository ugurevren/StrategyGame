using GridSystem;
using Helpers;
using UnityEngine;

namespace Building
{
    public class BuildingGhost : MonoBehaviour
    {
        private Transform _visual;
        private Poolable _poolable;
        private Vector3 _targetPosition;
        [SerializeField] private GridTester _gridTester;
        [SerializeField] private IsPointerOverUI _isPointerOverUI;

        private void Start()
        {
            RefreshVisual();
        
        }
        private void LateUpdate()
        {
            if (_poolable == null)
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
        private void RefreshVisual()
        {
            if (_poolable != null)
            {
                Destroy(_visual.gameObject);
            }
            _poolable = _gridTester.selectedBuilding;
            if (_poolable == null)
            {
                return;
            }
            _visual = Instantiate(_poolable.ghostVisual, _targetPosition, Quaternion.identity);
        }
    }
}
