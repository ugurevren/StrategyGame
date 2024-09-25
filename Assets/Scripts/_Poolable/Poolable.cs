using UnityEngine;
using UnityEngine.Events;

namespace _Poolable
{
    public class Poolable : MonoBehaviour
    {
        public GameObject prefab;
        protected Vector2Int _origin;

        public UnityEvent OnClick;
        public UnityEvent OnRightClick;

        public void Awake()
        {
            OnClick = new UnityEvent();
            OnRightClick = new UnityEvent();
        }
    
        public void HandleClick()
        {
            OnClick?.Invoke();
        }
    
        public void HandleRightClick()
        {
            OnRightClick?.Invoke();
        }

        public static Poolable Create(Vector3 worldPosition, Vector2Int origin, Poolable obj)
        {
            var transform = Instantiate(obj.prefab, worldPosition, Quaternion.identity);
            var placedObject = transform.GetComponent<Poolable>();
        
            placedObject._origin = origin;
            return placedObject;
        }
        public void Reinitialize(Vector3 worldPosition, Vector2Int origin)
        {
            // Reinitialize the object with new parameters
            transform.position = worldPosition;
            _origin = origin;
        }
    }
}
 