namespace ACO.Net.DataFormat
{
    public interface Converter<T>
    {
        string GoString(T source);
        T FromString(string str);
        string GetErrorMessage(T source);
        void ApplyCredentials(ref T source, Credentials cr);
        T Pack<A>(A from) where A : class;
        A Unpack<A>(T pm) where A : class;
    }
}