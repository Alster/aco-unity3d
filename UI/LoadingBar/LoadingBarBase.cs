using UnityEngine;
using UnityEngine.Events;
namespace ACO.UI
{
    public abstract class LoadingBarBase : UnityEngine.MonoBehaviour
    {
        public UnityEvent onBegin = new UnityEvent();
        public UnityEvent onFinish = new UnityEvent();
        [SerializeField]
        [Range(0, 1f)]
        float _progress = 0;
        public float progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                Apply();
            }
        }
        void OnValidate()
        {
            progress = progress;
        }
        protected abstract void Apply();
        public void Begin()
        {
            onBegin.Invoke();
            OnBegin();
        }
        public void Finish()
        {
            onFinish.Invoke();
            OnFinish();
        }
        protected abstract void OnBegin();
        protected abstract void OnFinish();
    }
}