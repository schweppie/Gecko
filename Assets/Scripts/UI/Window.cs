using UnityEngine;

namespace UI
{
    public abstract class Window : MonoBehaviour
    {
        public virtual void Initialize()
        {
            Debug.Log("Initialize window " + transform.name);
        }

        public abstract int GetLayer();

        public virtual void Show()
        {
            Debug.Log("Show window " + transform.name);
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            Debug.Log("Hide window " + transform.name);
            gameObject.SetActive(false);
        }
    }
}
