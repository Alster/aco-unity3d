using System.Collections;
using UnityEngine;

namespace ACO
{
    public class Singleton<T> : UnityEngine.MonoBehaviour where T : UnityEngine.MonoBehaviour
    {
        private static T _instance;
        private static object _lock = new object();

        public static T get
        {
            get
            {
#if !UNITY_EDITOR
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed on application quit." +
                        " Won't create again - returning null.");
                    return null;
                }
#endif
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it.");
                            return _instance;
                        }
                    }
                    return _instance;
                }
            }
        }
#if !UNITY_EDITOR
        private static bool applicationIsQuitting = false;
        public void OnDestroy()
        {
            applicationIsQuitting = true;
        }
#endif

        static public void DoCoroutine(IEnumerator e)
        {
            get.StartCoroutine(e);
        }
    }
}