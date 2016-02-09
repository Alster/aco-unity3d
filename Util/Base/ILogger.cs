namespace ACO.Util.Base
{
    public interface ILogger
    {
        void Begin(string evt);
        void Finish(string evt);
    }
}