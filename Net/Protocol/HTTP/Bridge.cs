#define LOG
using System.Collections;
using UnityEngine;

namespace ACO.Net.Protocol.HTTP
{
    public abstract class Bridge<T> : Protocol.Bridge<T> where T : class
    {
        static Bridge()
        {
            addressSeparator = "/";
            baseErrors.Add("4004", "Server or connection error.");
            baseErrors.Add("notAuthorized", "You isn't authorized");
            baseErrors.Add("invalidToken", "Auth token is invalid");
            baseErrors.Add("expiredAuthToken", "Auth token is expired");
        }
        public Bridge(DataFormat.Converter<T> dataConverter, Config config, Net.Protocol.HTTP.Connector connector) : base(dataConverter, config)
        {
            this.connector = connector;
        }
        protected Net.Protocol.HTTP.Connector connector;
        protected override void EmitBase(string message, string data, System.Action<string> callback)
        {
            ACO.Core.DoCoroutine(EmitBaseCoroutine(message, data, callback));
        }
        IEnumerator EmitBaseCoroutine(string message, string data, System.Action<string> callback)
        {
            WWWForm f = new WWWForm();
            f.AddField("data", data != null ? data.ToString() : "");

            /*JSONObject authData = new JSONObject(JSONObject.Type.OBJECT);
            authData.AddField("token", ACO.Core.get.userProfile.cloudToken);
            f.AddField("auth", authData.ToString());
            f.AddField("auth", credentials.ToString());*/
#if LOG
            Debug.Log(System.String.Format(
                    "WWW send : METHOD [{0}] : DATA [{1}]",
                    message, data
                    ));
#endif
            WWW w = new WWW(System.String.Format(
                "{0}:{1}/{2}",
                connector.address, connector.port, message
                ), f);
            yield return w;
            while (w.isDone == false)
                yield return null;

#if LOG
            Debug.Log(System.String.Format(
                    "WWW rec : METHOD [{0}] : DATA [{1}]",
                    message, w.text
                    ));
#endif

           /* if (w.error != null)
            {
                JSONObject jerror = new JSONObject(JSONObject.Type.OBJECT);
                jerror.AddField("res", "4004");
                callback(jerror);
            }
            else
            {*/
                callback(w.text);
            //}
        }
    }
}