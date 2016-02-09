using System;

namespace ACO
{
    public class Random
    {
        //private static long get_msec()
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime nowMsec0 = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        //    long TotalMsec = (long)((DateTime.Now - nowMsec0).TotalMilliseconds);

        //    return TotalMsec;
        //}

        static System.Random rnd = new System.Random();

        public static int Range(int maxval)
        {
            //var seed = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);


            //maxval -= 1;
            //long msec = get_msec();
            //return (int)(msec % (maxval + 1));

            return rnd.Next(0, maxval);
        }
        public static int Range(int minVal, int maxval)
        {
            return rnd.Next(minVal, maxval);
        }
    }
}