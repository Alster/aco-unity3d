using System.Collections;
using UnityEngine;

namespace ACO.UI
{
    [RequireComponent(typeof(WindowBase))]
    [RequireComponent(typeof(CanvasGroup))]
    [ExecuteInEditMode]
    public sealed class WindowSimple : UnityEngine.MonoBehaviour, IWindowReciever
    {
        public float openTime = 0, closeTime = 0;
        public bool removeOnClose = false;
        [HideInInspector]
        [SerializeField]
        WindowBase reference;
        [HideInInspector]
        [SerializeField]
        CanvasGroup canvasGroup;
        void OnValidate()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    Debug.LogError("CanvasGroup is not finded on this object", this);
                }
            }
            if (reference == null)
            {
                reference = GetComponent<WindowBase>();
                if (reference == null)
                {
                    Debug.LogError("WindowBase is not finded on this object", this);
                }
            }
        }

        public void OnOpen()
        {
            canvasGroup.interactable = false;
            if (!gameObject.activeSelf) { gameObject.SetActive(true); }
            canvasGroup.alpha = 1f; canvasGroup.blocksRaycasts = true;
            StartCoroutine(OpenCoroutine());
        }
        public void OnClose()
        {
            reference.Deactivate();
            StartCoroutine(CloseCoroutine());
        }
        public void OnBack() { }
        public void OnActivate()
        {
            canvasGroup.interactable = true;
        }
        public void OnDeactivate()
        {
            canvasGroup.interactable = false;
        }
        IEnumerator OpenCoroutine()
        {
            yield return new WaitForSeconds(openTime);
            if (reference.opened)
            {
                reference.Activate();
            }
        }
        IEnumerator CloseCoroutine()
        {
            yield return new WaitForSeconds(closeTime);
            if (!reference.opened)
            {
                canvasGroup.alpha = 0; canvasGroup.blocksRaycasts = false;
                if (removeOnClose)
                {
                    Destroy(gameObject);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}