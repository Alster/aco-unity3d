#define ACO_DEBUG

using SocketIO;
using UnityEngine.Assertions;
using System.Threading;

namespace ACO.Net.Protocol.SocketIO
{
    public class Bridge<T> : Protocol.Bridge<T> where T : class
    {
        static Bridge()
        {
            baseErrors.Add("authDataEmpty", "Incorrect authorization data");
            baseErrors.Add("noUserFinded", "Failed to authorize player");
        }
        public Bridge(DataFormat.Converter<T> dataConverter, Config config, Net.Protocol.SocketIO.Connector connector) : base(dataConverter, config)
        {
            this.connector = connector;

            this.connector.onOpen -= Init;
            this.connector.onOpen += Init;

            Init();
        }
        protected Net.Protocol.SocketIO.Connector connector;

        public virtual void Init()
        {
            if (this.connector.socket != null)
            {
                Init(this.connector.socket);
            }
        }
        public virtual void Init(SocketIOComponent socket)
        {
            //UnityEngine.Debug.Log("Bridge initialized: "+prefix);
            Assert.AreNotEqual(socket, null);
        }

        protected override void RecieveBase(string act, System.Action<string> callback)
        {
            connector.Subscribe(act, callback);
        }
        protected override void EmitBase(string message, string data, System.Action<string> callback)
        {
            ACO.ErrorCollector err = new ErrorCollector();
            JSONObject j = JSONObject.Create(JSONObject.Type.OBJECT);
            JSONObject testDataissue = new JSONObject(data);
            if (testDataissue.ToString() == "null")
            {
                j.AddField("message", data);
            }
            else
            {
                j.AddField("message", testDataissue);
            }
            //UnityEngine.Debug.Log("PRESEND FINAL DATA: " + j.ToString());
            string nnn = Thread.CurrentThread.Name;
            this.connector.socket.Emit(message, j, (res) => {
                //UnityEngine.Debug.Log(res.ToString());
                string nnnn = Thread.CurrentThread.Name;
                if (callback != null)
                {
                    callback(res.ToString());
                }
            });
        }
        //protected void Emit(
        //    string act, 
        //    JSONObject j,
        //    System.Action<JSONObject> onSuccess,
        //    System.Action<JSONObject> onFail,
        //    System.Action<JSONObject> onRecieve = null, 
        //    string logMessage = ""
        //    )
        //{
        //    Emit(act, j, (res) =>
        //    {
        //        if (onRecieve != null)
        //        {
        //            onRecieve(res);
        //        }
        //        if (IsOk(res))
        //        {
        //            if (onSuccess != null)
        //            {
        //                onSuccess(res);
        //            }
        //        }
        //        else
        //        {
        //            if (onFail != null)
        //            {
        //                onFail(res);
        //            }
        //        }
        //    }, logMessage);
        //}
        //protected void Emit(
        //    string act,
        //    JSONObject j,
        //    Dictionary<string, string> errors, 
        //    System.Action<JSONObject> onSuccess,
        //    System.Action<JSONObject> onFail,
        //    System.Action<JSONObject> onRecieve = null,
        //    string logMessage = ""
        //    )
        //{
        //    Emit(act, j, onSuccess, (res) => {
        //        ProcessFail(res, errors, (msg) => {
        //            if (onFail != null)
        //            {
        //                onFail(res);
        //            }
        //        }, prefix + ":" + act);
        //    }, 
        //    onRecieve, logMessage);
        //}
        //protected bool IsOk(JSONObject msg)
        //{
        //    return msg[responseName].str == "ok";
        //}
        //protected void ProcessFail(JSONObject msg, Dictionary<string, string> dict, System.Action<string> action, string additionalInfo)
        //{
        //    string res = msg[responseName].str;
        //    Assert.AreNotEqual(socket, null);
        //    string message = "Unknown error: ["+additionalInfo+"]: " + res;
        //    if (dict.ContainsKey(res))
        //    {
        //        message = dict[res];
        //    }
        //    else
        //    {
        //        UnityEngine.Debug.LogError(message);
        //    }
        //    if (action != null)
        //    {
        //        action(res);
        //    }
        //    LogError(message);
        //}
        /*public void SetLogger(System.Action<string> add, System.Action<string> end)
        {
            LogAdd = add; LogEnd = end;
        }
        public void SetErrorLogger(System.Action<string> log)
        {
            LogError = log;
        }
        public void ResetLogger()
        {
            LogAdd = def_logAdd;
            LogEnd = def_logEnd;
        }
        public void ResetErrorLogger()
        {
            LogError = def_logError;
        }*/
    }
}