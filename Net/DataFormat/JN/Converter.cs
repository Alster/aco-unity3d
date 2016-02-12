using Newtonsoft.Json;

namespace ACO.Net.DataFormat.JN
{
    public class Converter : DataFormat.Converter<JSONObject>
    {
        static readonly string responseName = "res";

        public JSONObject FromString(string str)
        {
            //UnityEngine.Debug.Log("converter: " + str);
            //UnityEngine.Debug.Log("converter result: " + JSONObject.Create(str).ToString());
            return JSONObject.Create(str)[0];
        }

        public string GoString(JSONObject source)
        {
            //UnityEngine.Debug.Log("converter: " + source.ToString());
            return source.ToString();
        }

        public string GetErrorMessage(JSONObject source)
        {
            //UnityEngine.Debug.Log(source.ToString());
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
            return JsonConvert.SerializeObject(source);
        }
        JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        T Deserialize<T>(string source)
        {
            settings.NullValueHandling = NullValueHandling.Include;
            settings.MissingMemberHandling = MissingMemberHandling.Error;
            return JsonConvert.DeserializeObject<T>(source, settings);
        }
    }
}