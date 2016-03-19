using UnityEngine;
using System.Collections.Generic;

namespace ACO.UI
{
    public class WindowTabs : MonoBehaviour
    {
        public GameObject selected;
        List<WindowTab> tabs = new List<WindowTab>();
        void Awake()
        {
            tabs = ACO.Utility.GetComponentsSortedToList<WindowTab>(transform);
            Select(selected);
        }
        public void Select(GameObject win)
        {
            selected.SetActive(false);
            selected = win;
            foreach (var v in tabs)
            {
                if (v.window == win)
                {
                    v.Selected();
                }
                else
                {
                    v.Deselected();
                }
            }
            selected.SetActive(true);
        }
    }
}