namespace ACO.Util.MTDispatcher
{
    public class DispatcherJSON : MTDispatcher<JSONObject>
    {
        public void Add(System.Action<JSONObject> act, JSONObject msg)
        {
            AddTask(act, msg);
        }
    }
}