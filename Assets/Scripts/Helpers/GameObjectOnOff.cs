using UnityEngine;

namespace Helpers
{
    public class GameObjectOnOff : MonoBehaviour
    {
        public void ToggleActive()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
