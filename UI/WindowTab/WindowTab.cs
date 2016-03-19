using UnityEngine;
using UnityEngine.UI;

namespace ACO.UI
{
    public class WindowTab : MonoBehaviour
    {
        public Button button;
        public GameObject window;
        WindowTabs controller;
        public Image image;
        public Color selected, unselected;
        void Awake()
        {
            controller = ACO.Utility.FindParent<WindowTabs>(transform);
            button.onClick.RemoveListener(OnClick); button.onClick.AddListener(OnClick);
        }
        void OnClick()
        {
            controller.Select(window);
        }
        public void Selected()
        {
            image.color = selected;
        }
        public void Deselected()
        {
            image.color = unselected;
        }
    }
}