using UnityEngine;

namespace GridSystem
{
    public class Grid<TGridObject>
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _originPosition;
        private TGridObject[,] _gridArray;
        private TextMesh[,] _debugTextArray;
        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;
        
            _gridArray = new TGridObject[width, height];
            _debugTextArray = new TextMesh[width, height];
        
            for(int x=0;x < _gridArray.GetLength(0); x++)
            {
                for(int y=0; y < _gridArray.GetLength(1); y++)
                {
                    _debugTextArray[x,y]=CreateWorldText(_gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + 
                        new Vector3(_cellSize, _cellSize) * 0.5f, 20, Color.white);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
        }
        
        private void SetValue(int x, int y, TGridObject value)
        {
            if(x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
                _debugTextArray[x, y].text = _gridArray[x, y].ToString();
            }
        }
        public void SetValue(Vector3 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetValue(x, y, value);
        }
        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition-_originPosition).x / _cellSize);
            y = Mathf.FloorToInt((worldPosition-_originPosition).y / _cellSize);
        }
    
        private static TextMesh CreateWorldText(string text, Transform parent = null, 
            Vector3 localPosition = default(Vector3), int fontSize = 40, Color color = default(Color), 
            TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center, 
            int sortingOrder = 0)
        {
            if (color == default(Color)) color = Color.white;
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + _originPosition;
        }
        public TGridObject GetValue(int x, int y)
        {
            if(x >= 0 && y >= 0 && x < _width && y < _height)
            {
                return _gridArray[x, y];
            }
            return default;
        }
        
        public TGridObject GetValue(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetValue(x, y);
        }
    }
}
