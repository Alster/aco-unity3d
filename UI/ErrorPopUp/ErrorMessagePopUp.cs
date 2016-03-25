using UnityEngine;

namespace ACO.UI
{
    public class ErrorMessagePopUp : MonoBehaviour
    {
        public ErrorMessagePopUpInstance prefab;
        public Transform errorsPlace;
        
        public void Raise(string message, string details = "")
        {
            ErrorMessagePopUpInstance ins = Instantiate(prefab) as ErrorMessagePopUpInstance;
            ins.transform.SetParent(errorsPlace, false);
            ins.transform.localPosition = Vector3.zero;
            ins.Apply(message, details);
            Debug.LogError(message + " => " + details);
        }

        public void Raise(Error err)
        {
            Raise(err.Message, err.Details);
        }
    }
}