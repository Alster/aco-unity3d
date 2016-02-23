using ProtoBuf;

namespace ACO.Net.DataFormat.Protobuf
{
    public class Converter : Converter<JSONObject>
    {
        static readonly string responseName = "res";

        public JSONObject FromString(string str)
        {
            return JSONObject.Create(str)[0];
        }
        public JSONObject FromStringRecieve(string str)
        {
            return JSONObject.Create(str);
        }

        public string GoString(JSONObject source)
        {
            return source.ToString();
        }

        public string GetErrorMessage(JSONObject source)
        {
            if (!source.HasField(responseName))
            {
                return "noField";
            }
            return source[responseName].str;
        }

        public void ApplyCredentials(ref JSONObject source, string token)
        {
            source.AddField("token", token);
        }
        public JSONObject Pack<T>(T from) where T : class
        {
            JSONObject pm = JSONObject.Create(JSONObject.Type.OBJECT);
            pm.AddField("data", JSONObject.Create(Serialize(from)));
            return pm;
        }
        public T Unpack<T>(JSONObject pm) where T : class
        {
            return Deserialize<T>(pm["data"].ToString());
        }
        string Serialize<T>(T source)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, source);
                return System.Convert.ToBase64String(stream.ToArray());
            }
        }
        T Deserialize<T>(string source)
        {
            using (var stream = new System.IO.MemoryStream(System.Convert.FromBase64String(source)))
            {
                return Serializer.Deserialize<T>(stream);
            }
        }
    }
}