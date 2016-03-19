using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace ACO.UI
{
    public class StatusBar : MonoBehaviour
    {
        public Text text;
        public Text testLog;

        public List<string> tasks = new List<string>();

        public string log;

        public void LogAdd(string msg)
        {
            tasks.Add(msg);
            Updatelog();
            log += "+" + msg + "\n";
        }
        public void LogEnd(string msg)
        {
            tasks.Remove(msg);
            Updatelog();
            log += "-" + msg + "\n";
        }
        void Updatelog()
        {
            string res = "";
            res = System.String.Join("\n", tasks.ToArray());
            text.text = res;
            testLog.text = log;
        }
    }
}