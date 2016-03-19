using UnityEngine;
using UnityEngine.Events;

namespace ACO.UI
{
    public class InputReciever : MonoBehaviour {
        
        public UnityEvent OnRaise = new UnityEvent();
        public string Key = "Submit";
        private WindowBase _parent;

        private void Awake()
        {
            _parent = ACO.Utility.FindParent<WindowBase>(transform);
        }
        private void Update()
        {
            if ((_parent != null && _parent.activated && Input.GetButtonDown(Key)) || (_parent == null && Input.GetButtonDown(Key)))
            {
                OnRaise.Invoke();
            }
        }
    }
}
