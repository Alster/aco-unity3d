using System.Collections.Generic;

namespace ACO
{
    public class ErrorCollector
    {
        public List<string> errors = new List<string>();
        public void Assert(bool condition, string msg)
        {
            if (!condition)
            {
                errors.Add(msg);
                UnityEngine.Debug.LogWarning(msg);
            }
        }
        public void Assert(System.Func<bool> condition, string msg)
        {
            if (!condition())
            {
                errors.Add(msg);
                UnityEngine.Debug.LogWarning(msg);
            }
        }
        public bool HasError()
        {
            return errors.Count > 0;
        }
        public string GetFirst()
        {
             return errors.Count > 0 ? errors[0] : "";
        }
        public void ThrowOne(System.Action<string> act)
        {
            if (errors.Count == 0)
            {
                return;
            }
            act(errors[0]);
        }
        public void ThrowAll(System.Action<string> act, string separator = ",")
        {
            if (errors.Count == 0)
            {
                return;
            }
            act(GetJoined(separator));
        }
        public string GetJoined(string separator = ",")
        {
            return string.Join(separator, errors.ToArray());
        }
    }
}