namespace ACO.EIMT
{
    public class Executer : EIMT
    {
        public static void Add(System.Action act)
        {
            AddTask(act);
        }
    }
}