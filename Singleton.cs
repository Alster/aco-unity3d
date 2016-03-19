using System.Collections;
using UnityEngine;

namespace ACO
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static void SetInstance(T inst)
        {
            _instance = inst;
        }
        private static T _instance;
        private static readonly object _lock = new object();

        public static T Get
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
                    if (_instance != null) return _instance;
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (FindObjectsOfType(typeof (T)).Length <= 1) return _instance;
                    Debug.LogError("[Singleton] Something went really wrong " +
                                   " - there should never be more than 1 singleton!" +
                                   " Reopening the scene might fix it.");
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

        public static Coroutine DoCoroutine(IEnumerator e)
        {
            return Get.StartCoroutine(e);
        }
        public static void BreakCoroutine(Coroutine e)
        {
            Debug.Log("This is not joke");
            Get.StopCoroutine(e);
        }
    }
}