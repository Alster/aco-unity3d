using System.Collections.Generic;

namespace ACO
{
    public class Fortune<T>
    {
        List<T> list = new List<T>();
        public void Add(T v, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(v);
            }
        }
        public T Spin()
        {
            if (list.Count == 0)
            {
                throw new System.Exception("Fortune list is empty");
            }
            Utility.ShakeList<T>(ref list);
            return list[0];
        }
        public void Clear()
        {
            list.Clear();
        }
    }
}