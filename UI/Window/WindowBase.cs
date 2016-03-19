using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ACO.UI
{
    public interface IWindowReciever
    {
        void OnOpen();
        void OnClose();
        void OnBack();
        void OnActivate();
        void OnDeactivate();
    }
    public sealed class WindowBase : UnityEngine.MonoBehaviour
    {
        public bool manualBack = false, closePrevious = false;
        public WindowBase backWindow;
        public UnityEvent OnOpen = new UnityEvent();
        public UnityEvent OnClose = new UnityEvent();
        public UnityEvent OnBack = new UnityEvent();
        public UnityEvent OnActivate = new UnityEvent();
        public UnityEvent OnDeactivate = new UnityEvent();
        public bool enableCache = true;
        public static bool userControlEnabled = true;
        IWindowReciever[] cache = null;
        IWindowReciever mainReciever = null;

        delegate void EmptyIWindowReciever(IWindowReciever arg);
        void CallEvent(EmptyIWindowReciever rec)
        {
            if (mainReciever == null)
            {
                mainReciever = GetComponent<IWindowReciever>();
            }
            rec(mainReciever);
            if (cache == null || !enableCache || isCacheDirty)
            {
                cache = GetComponentsInChildren<IWindowReciever>();
                isCacheDirty = false;
            }
            /*IWindowReciever[] arr = GetComponents<IWindowReciever>();
            foreach (var v in arr){rec(v);}*/
            foreach (var v in cache)
            {
                if (v == mainReciever)
                {
                    continue;
                }
                rec(v);
            }
        }
        bool isCacheDirty = false;
        public void SetCacheDirty()
        {
            isCacheDirty = true;
        }

        private static Stack<WindowBase> stack = new Stack<WindowBase>();
        public static Stack<WindowBase> GetStack()
        {
            return stack;
        }
        private bool _activated = false;
        public bool activated { get { return _activated; } private set { _activated = value; } }
        private bool _opened = false;
        public bool opened { get { return _opened; } private set { _opened = value; } }
        public void Open()
        {
            if (opened) { return; }
            if (closePrevious)
            {
                if (stack.Count > 0)
                {
                    if (stack.Peek() == this)
                    {
                        Debug.LogError(string.Format(
                            "You trying to autoClosePrevious this window {(0)}",
                            this.gameObject.name
                        ), this);
#if UNITY_EDITOR
                        EditorUtility.SetDirty(this);
#endif
                        return;
                    }
                    stack.Peek().Close();
                }
            }
            if (stack.Count != 0)
            {
                foreach (var item in stack)
                {
                    if (item == this)
                    {
                        Debug.LogError(string.Format(
                            "You trying to reopen this window {(0)}",
                            this.gameObject.name
                        ), this);
#if UNITY_EDITOR
                        EditorUtility.SetDirty(this);
#endif
                        return;
                    }
                }
                foreach (var item in stack)
                {
                    if (item.activated)
                    {
                        item.Deactivate();
                    }
                }
            }
            opened = true;
            activated = false;
            stack.Push(this);
            CallEvent(v => v.OnOpen()); OnOpen.Invoke();
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void Close()
        {
            if (!opened) { return; }
            if (stack.Count != 0)
            {
                WindowBase temp = stack.Peek();
                if (temp != this)
                {
                    Debug.LogError(string.Format(
                        "Wait for closing another window ({0}) in the stack, before closing this ({1})",
                        temp.gameObject.name, this.gameObject.name
                    ), this);
                }
                else
                {
                    opened = false;
                    stack.Pop();
                    CallEvent(v => v.OnClose()); OnClose.Invoke();
                    activated = false;
                    if (stack.Count != 0)
                    {
                        stack.Peek().Activate();
                    }
                }
            }
            else
            {
                Debug.LogError(string.Format(
                    "Windows stack is empty ({0})",
                    this.gameObject.name
                    ), this);
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void Activate()
        {
            if (!activated)
            {
                activated = true;
                CallEvent(v => v.OnActivate()); OnActivate.Invoke();
            }
            else
            {
                Debug.LogError(string.Format(
                    "This window ({0}) is already active",
                    this.gameObject.name
                    ), this);
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void Deactivate()
        {
            if (activated)
            {
                activated = false;
                CallEvent(v => v.OnDeactivate()); OnDeactivate.Invoke();
            }
            else
            {
                Debug.LogError(string.Format(
                    "This window ({0}) is already unactive",
                    this.gameObject.name
                    ), this);
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void Back()
        {
            if (!opened) { return; }
            if (stack.Count != 0)
            {
                WindowBase temp = stack.Peek();
                if (temp != this)
                {
                    Debug.LogError(string.Format(
                        "Wait for closing another window in the stack ({0}), before closing this ({1})",
                        temp.gameObject.name, this.gameObject.name
                        ), this);
                }
                else
                {
                    CallEvent(v => v.OnBack()); OnBack.Invoke();
                    opened = false;
                    stack.Pop();
                    CallEvent(v => v.OnClose()); OnClose.Invoke();
                    activated = false;
                    if (backWindow != null)
                    {
                        if (stack.Count != 0)
                        {
                            bool backWindowIsInStack = false;
                            foreach (var item in stack)
                            {
                                if (item == backWindow)
                                {
                                    backWindowIsInStack = true;
                                    break;
                                }
                            }
                            if (!backWindowIsInStack) { backWindow.Open(); }
                            else if (backWindowIsInStack && stack.Peek() == backWindow)
                            {
                                backWindow.Activate();
                            }
                            else
                            {
                                Debug.LogError(string.Format(
                                    "You trying to open backWindow ({0}), but he is already opened and placed deep in stack. He must be placed in the top in stack. My name is ({1})",
                                    backWindow.gameObject.name, this.gameObject.name
                                ), this);
                            }
                        }
                        else { backWindow.Open(); }
                    }
                    else if (stack.Count != 0)
                    {
                        stack.Peek().Activate();
                    }
                }
            }
            else { Debug.LogError("Windows stack is empty", this); }
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        void Update()
        {
            if (userControlEnabled && !manualBack && activated && Input.GetKeyDown(KeyCode.Escape))
            {
                this.Back();
            }
        }
        public void CloseAllWindows()
        {
            while (stack.Count > 0)
            {
                stack.Peek().Close();
            }
        }
        public static void CloseAllWindowsStatic()
        {
            if (stack.Count > 0)
            {
                stack.Peek().CloseAllWindows();
            }
        }
        public static WindowBase GetParentWindow(Transform tr)
        {
            return ACO.Utility.FindParent<WindowBase>(tr) as WindowBase;
        }
    }
}