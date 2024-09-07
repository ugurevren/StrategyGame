using GridSystem;
using UnityEngine;


public class GridTester : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector3 _originPosition;
    private Grid<bool> _grid;
    
    private void Start()
    {
         _grid = new Grid<bool>(_width, _height, _cellSize, _originPosition);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _grid.SetValue(GetMouseWorldPosition(), true);
        }
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(_grid.GetValue(GetMouseWorldPosition()));
        }
    }
    public Vector3 GetMouseWorldPosition()
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}
