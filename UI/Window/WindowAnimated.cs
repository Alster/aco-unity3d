using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ACO.UI
{
    [RequireComponent(typeof(WindowBase))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Animator))]
    [ExecuteInEditMode]
    public sealed class WindowAnimated : UnityEngine.MonoBehaviour
    {
        /*WindowBase reference;
        CanvasGroup canvasGroup;
        Animator animator;
        void OnValidate(){
            if (canvasGroup == null){
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null){
                    Debug.LogError("CanvasGroup is not finded on this object", this);
                }
            }
            if (animator == null){
                animator = GetComponent<Animator>();
                if (animator == null){
                    Debug.LogError("Animator is not finded on this object", this);
                }
            }
            if (reference == null){
                reference = GetComponent<WindowBase>();
                if (reference == null){
                    Debug.LogError("WindowBase is not finded on this object", this);
                }
                reference.onActivate.RemoveListener(Activate); reference.onActivate.AddListener(Activate);
                reference.onDeactivate.RemoveListener(Deactivate); reference.onDeactivate.AddListener(Deactivate);
                reference.onOpen.RemoveListener(OpenCustom); reference.onOpen.AddListener(OpenCustom);
                reference.onClose.RemoveListener(CloseCustom); reference.onClose.AddListener(CloseCustom);
            }
        }

        void OpenCustom(){
            gameObject.SetActive(true);
            animator.Play("Open");
        }
        void CloseCustom(){
            reference.Deactivate();
            animator.Play("Close");
        }
        public void Activate(){
            canvasGroup.interactable = true;
            animator.Play("Activate");
        }
        public void Deactivate(){
            canvasGroup.interactable = false;
            animator.Play("Deactivate");
        }*/
    }
}