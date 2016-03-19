using UnityEngine;
using UnityEngine.UI;

namespace ACO.UI
{
    public class LoadingBar_Simple : LoadingBarBase
    {
        public Text text, text2;
        public RectTransform bar;

        float parentWidth = 0;
        string resText = "";
        protected override void Apply()
        {
            resText = (100f * progress).ToString("#00");
            text.text = resText; if (text2 != null) { text2.text = resText; }

            if (bar != null)
            {
                parentWidth = (transform.parent as RectTransform).sizeDelta.x;
                bar.sizeDelta = new Vector2(-parentWidth * (1f - progress), bar.sizeDelta.y);
            }
        }
        protected override void OnBegin() { }
        protected override void OnFinish() { }
    }
}