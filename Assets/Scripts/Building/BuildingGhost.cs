
using Building;
using GridSystem;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private Transform _visual;
    private UnitSO _unitSo;
    private Vector3 _targetPosition;
    [SerializeField] private GridTester _gridTester;

    private void Start()
    {
        RefreshVisual();
        //Grid<GridObject>.OnSelectedChanged += Instance_OnSelectedChanged;
    }
    private void Instance_OnSelectedChanged(object sender, System.EventArgs e)
    {
        RefreshVisual();
    }

    private void LateUpdate()
    {
        if (_unitSo == null)
        {
            return;
        }
        //var mousePos = GetMouseWorldPosition();
        //_targetPosition = new Vector3(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y), 0);
        //_visual.position = _targetPosition;
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
