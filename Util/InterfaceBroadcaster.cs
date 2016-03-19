using System.Linq;
using UnityEngine;

namespace ACO.Util
{
    [System.Serializable]
    public class InterfaceBroadcaster<T> where T : class
    {
        public bool EnableCache;

        private T _mainReciever;
        private readonly GameObject _root;
        private T[] _cache = null;
        private bool _isCacheDirty = true;

        public InterfaceBroadcaster(GameObject root, bool enableCache = false)
        {
            EnableCache = enableCache;
            _root = root;
        }

        public void CallEvent(System.Action<T> rec)
        {
            if (_mainReciever == null)
            {
                _mainReciever = _root.GetComponent<T>();
            }
            if (_mainReciever != null)
            {
                rec(_mainReciever);
            }
            if (_cache == null || !EnableCache || _isCacheDirty)
            {
                _cache = _root.GetComponentsInChildren<T>(true);
                _isCacheDirty = false;
            }
            /*IWindowReciever[] arr = GetComponents<IWindowReciever>();
            foreach (var v in arr){rec(v);}*/
            foreach (var v in _cache.Where(v => v != _mainReciever))
            {
                rec(v);
            }
        }
    }
}