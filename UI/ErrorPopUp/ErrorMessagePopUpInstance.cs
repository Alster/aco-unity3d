using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ACO.UI
{
    public class ErrorMessagePopUpInstance : MonoBehaviour
    {
        public Text textMessage, detailsMessage;
        public WindowBase window;

        public void Apply(string text, string details)
        {
            textMessage.text = text;
            detailsMessage.text = details;
            window.Open();
        }
    }
}