using UnityEngine;
using UnityEngine.Events;

namespace ACO.Helper
{
    public class ObjectActivator : MonoBehaviour
    {
        public bool DefaultState = true;
        public bool InvertEvent = false;

        public UnityEvent OnActivate = new UnityEvent();
        public UnityEvent OnDeactivate = new UnityEvent();

        private bool _activatorChanged = false;

        private void Awake()
        {
            if (_activatorChanged) return;
            gameObject.SetActive(DefaultState);
        }

        public void Activate(bool state)
        {
            _activatorChanged = true;
            var res = InvertEvent ? !state : state;
            gameObject.SetActive(res);
            if (res)
            {
                OnActivate.Invoke();
            }
            else
            {
                OnDeactivate.Invoke();
            }
        }
    }
}