using System;
using UnityEngine;

namespace GridSystem
{
    public class Grid<TGridObject>
    {
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
        public class OnGridObjectChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }

        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _originPosition;
        private TGridObject[,] _gridArray;
        private TextMesh[,] _debugTextArray;
        public Grid(int width, int height, float cellSize, Vector3 originPosition,
            Func<Grid<TGridObject>,int,int,TGridObject> createGridObject, bool showDebug = false)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;
        
            _gridArray = new TGridObject[width, height];
            
        
            for(int x=0;x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    _gridArray[x, y] = createGridObject(this, x, y);
                    
                }
            }

            if (showDebug)
            {
                _debugTextArray = new TextMesh[width, height];
                for (int x = 0; x < _gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < _gridArray.GetLength(1); y++)
                    {
                        _debugTextArray[x, y] = CreateWorldText(_gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * 0.5f, 30, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
                
            }
        }
        
        public void TriggerGridObjectChanged(int x, int y)
        {
            if(OnGridObjectChanged!= null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs {x = x, y = y});
        }
        
        private void SetGridObject(int x, int y, TGridObject value)
        {
            if(x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
                if(OnGridObjectChanged!= null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs {x = x, y = y});
            }
        }
        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }
        
        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + _originPosition;
        }
        public TGridObject GetGridObject(int x, int y)
        {
            if(x >= 0 && y >= 0 && x < _width && y < _height)
            {
                return _gridArray[x, y];
            }
            return default;
        }
        
        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }
        
        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition-_originPosition).x / _cellSize);
            y = Mathf.FloorToInt((worldPosition-_originPosition).y / _cellSize);
        }
        public int GetWidth()
        {
            return _width;
        }
        public int GetHeight()
        {
            return _height;
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
        
    }
}
