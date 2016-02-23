namespace ACO.Net
{
    [System.Serializable]
    public class Config
    {
        public System.Action<string> logBegin;
        public System.Action<string> logFinish;
        public System.Action<string, string> errorRaise;
        public string token;
    }
}