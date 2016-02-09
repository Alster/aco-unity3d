using UnityEngine;

namespace ACO.Util
{
    public class LoggerUnity : MonoBehaviour, ACO.Util.Base.ILogger
    {
        public void Begin(string evt)
        {
            Debug.Log(evt);
        }
        public void Finish(string evt)
        {

        }
    }
}