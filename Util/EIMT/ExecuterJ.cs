namespace ACO.EIMT
{
    public class ExecuterJ : EIMT<JSONObject>
    {
        public static void Add(System.Action<JSONObject> act, JSONObject msg)
        {
            AddTask(act, msg);
        }
    }
}