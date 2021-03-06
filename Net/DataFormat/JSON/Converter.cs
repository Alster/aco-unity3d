﻿namespace ACO.Net.DataFormat.JSON
{
    public class Converter : Converter<JSONObject>
    {
        static readonly string responseName = "res";

        public JSONObject FromString(string str)
        {
            UnityEngine.Debug.Log("converter: " + str);
            UnityEngine.Debug.Log("converter result: " + JSONObject.Create(str).ToString());
            return JSONObject.Create(str)[0];
        }
        public JSONObject FromStringRecieve(string str)
        {
            UnityEngine.Debug.Log("converter: " + str);
            UnityEngine.Debug.Log("converter result: " + JSONObject.Create(str).ToString());
            return JSONObject.Create(str);
        }

        public string GoString(JSONObject source)
        {
            UnityEngine.Debug.Log("converter: " + source.ToString());
            return source.ToString();
        }

        public string GetErrorMessage(JSONObject source)
        {
            UnityEngine.Debug.Log(source.ToString());
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
            if (typeof(T) != typeof(JSONObject))
            {
                throw new System.InvalidCastException("Packed message must be a JSONObject");
            }
            JSONObject _from = from as JSONObject;
            JSONObject pm = JSONObject.Create(JSONObject.Type.OBJECT);
            pm.AddField("data", _from);
            return pm;
        }
        public T Unpack<T>(JSONObject pm) where T : class
        {
            if (typeof(T) != typeof(JSONObject))
            {
                throw new System.InvalidCastException("Packed message must be a JSONObject");
            }
            return JSONObject.Create(pm["data"].ToString()) as T;
        }
    }
}