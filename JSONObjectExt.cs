using System.Collections.Generic;

namespace ACO
{
    public static class JSONObjectExt
    {
        public static void Each(this JSONObject j, System.Action<JSONObject> act)
        {
            if (!j.IsArray)
            {
                throw new System.Exception("This isn't array");
            }
            for (int i = 0; i < j.Count; i++)
            {
                act(j[i]);
            }
        }
        public static List<T> Each<T>(this JSONObject j, System.Func<JSONObject, T> f)
        {
            if (!j.IsArray)
            {
                throw new System.Exception("This isn't array");
            }
            List<T> res = new List<T>();
            for (int i = 0; i < j.Count; i++)
            {
                res.Add(f(j[i]));
            }

            return res;
        }
    }
}