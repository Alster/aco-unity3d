namespace ACO.Util.MTDispatcher
{
    public class Dispatcher : MTDispatcher
    {
        public static void Add(System.Action act)
        {
            AddTask(act);
        }
    }
}