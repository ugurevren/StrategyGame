
using Building;
using GridSystem;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private Transform _visual;
    private BuildingSO _buildingSo;
    private Vector3 _targetPosition;
    [SerializeField] private GridTester _gridTester;

    private void Start()
    {
        RefreshVisual();
        Grid<GridObject>.Instance.OnGridObjectChanged += Instance_OnSelectedChanged;
    }
    private void Instance_OnSelectedChanged(object sender, System.EventArgs e)
    {
        RefreshVisual();
    }

    private void LateUpdate()
    {
        if (_buildingSo == null)
        {
            return;
        }
        var mousePos = Grid<GridObject>.Instance.GetMouseWorldSnappedPosition();
        _targetPosition = new Vector3(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y), 0);
        _visual.position = _targetPosition;
    }
    private void RefreshVisual()
    {
        if (_buildingSo != null)
        {
            Destroy(_visual.gameObject);
        }
        _buildingSo = _gridTester.selectedBuilding;
        if (_buildingSo == null)
        {
            return;
        }
        _visual = Instantiate(_buildingSo.visual, _targetPosition, Quaternion.identity);
    }
}
