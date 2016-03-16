namespace ACO.MTDispatcher
{
    public class DispatcherJSON : MTDispatcher<JSONObject>
    {
        public static void Add(System.Action<JSONObject> act, JSONObject msg)
        {
            AddTask(act, msg);
        }
    }
}