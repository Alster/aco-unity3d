using System.Collections.Generic;

namespace ACO
{
    public class ErrorCollector : UnityEngine.Object
    {
        public List<string> errors = new List<string>();
        public ErrorCollector Equals(object o1, object o2, string msg = null)
        {
            if ((o1 == null && o2 != null) || !o1.Equals(o2))
            {
                if (msg != null)
                {
                    msg += "\n";
                }
                msg += string.Format("'{0}' != '{1}'", SafeToString(o1), SafeToString(o2));
                errors.Add(msg);
                //UnityEngine.Debug.LogWarning(msg);
            }
            return this;
        }
        public ErrorCollector NotEquals(object o1, object o2, string msg = null)
        {
            if ((o1 == null && o2 == null) || o1.Equals(o2))
            {
                if (msg != null)
                {
                    msg += "\n";
                }
                msg += string.Format("'{0}' == '{1}'", SafeToString(o1), SafeToString(o2));
                errors.Add(msg);
                //UnityEngine.Debug.LogWarning(msg);
            }
            return this;
        }
        public ErrorCollector Assert(bool condition, string msg)
        {
            if (!condition)
            {
                errors.Add(msg);
                //UnityEngine.Debug.LogWarning(msg);
            }
            return this;
        }
        public ErrorCollector Assert(System.Func<bool> condition, string msg)
        {
            if (!condition())
            {
                errors.Add(msg);
                //UnityEngine.Debug.LogWarning(msg);
            }
            return this;
        }
        public bool HasError()
        {
            return errors.Count > 0;
        }
        public void Throw(string prefix = "assert", System.Action<string> act = null, string separator = ", ")
        {
            if (errors.Count == 0)
            {
                return;
            }
            prefix += ": " + GetJoined(separator);
            if (act != null)
            {
                act(prefix);
            }
            else {
                throw new System.Exception(prefix);
            }
        }
        public string GetJoined(string separator = ",")
        {
            return string.Join(separator, errors.ToArray());
        }
        string SafeToString(object o)
        {
            return o == null ? "null" : (o.GetType().Name + ": " + o.ToString());
        }
    }
}