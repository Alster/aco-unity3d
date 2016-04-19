using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace ACO.Net.DataFormat.JN
{
    public class Converter : Converter<JSONObject>
    {
        static readonly string errorFieldName = "err";
        static readonly string dataFieldName = "data";

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
            if (!source.HasField(errorFieldName))
            {
                return null;
            }
            return source[errorFieldName].str;
        }

        public void ApplyCredentials(ref JSONObject source, string token)
        {
            source.AddField("token", token);
        }
        public JSONObject Pack<T>(T from) where T : class
        {
            JSONObject pm = JSONObject.Create(JSONObject.Type.OBJECT);
            pm.AddField(dataFieldName, JSONObject.Create(Serialize(from)));
            return pm;
        }
        public T Unpack<T>(JSONObject pm) where T : class
        {
            return Deserialize<T>(pm[dataFieldName].ToString());
        }
        string Serialize<T>(T source)
        {
            return JsonConvert.SerializeObject(source);
        }
        JsonSchemaGenerator generator = new JsonSchemaGenerator();
        JsonSerializer serializer = new JsonSerializer();
        T Deserialize<T>(string source)
        {
            JsonSchema schema = generator.Generate(typeof(T));
            JsonTextReader reader = new JsonTextReader(new System.IO.StringReader(source));
            JsonValidatingReader validatingReader = new JsonValidatingReader(reader);
            validatingReader.Schema = schema;
            return serializer.Deserialize<T>(validatingReader);
        }
    }
}