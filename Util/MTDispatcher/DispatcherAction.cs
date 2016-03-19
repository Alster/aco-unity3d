namespace ACO.Util.MTDispatcher
{
    public class DispatcherAction : MTDispatcher
    {
        public void Add(System.Action act)
        {
            AddTask(act);
        }
    }
}