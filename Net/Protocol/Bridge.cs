//#define RELEASE
using System.Collections.Generic;

namespace ACO.Net.Protocol
{
    public abstract class Bridge <T> where T : class
    {
        public Bridge(DataFormat.Converter<T> dataConverter, Config config)
        {
            this.dataConverter = dataConverter;
            this.config = config;
        }

        DataFormat.Converter<T> dataConverter { get; set; }
        protected Config config { get; private set; }

        public string prefix { get; protected set; }
        static string messageError = "Protocol error";
        protected static Dictionary<string, string> baseErrors = new Dictionary<string, string>
        {
            {"cannotReadResult", messageError },
            {"noField", messageError },
            {"messageProcessFail", messageError },
            {"invalidMessage", messageError }
        };

        protected void LogShow(string s)
        {
            config.logBegin(s);
        }
        protected void LogHide(string s)
        {
            config.logFinish(s);
        }
        protected void LogError(string s, string details)
        {
            config.errorRaise(s, details);
        }

        protected static string addressSeparator = "/";
        protected static string errorEmpty = null;

        protected virtual void EmitBase(string message, string data, System.Action<string> callback) {}
        protected virtual void RecieveBase(string message, System.Action<string> callback) {
            throw new System.NotImplementedException("Recieving is not implemetded in this bridge");
        }
        protected void Recieve<RES>(string act, System.Action<RES> callback)
            where RES : class
        {
            RecieveBase(System.String.Format(
                "{0}{1}{2}",
                prefix, addressSeparator, act
                ), (res) =>
            {
#if RELEASE
                try {
#endif
                callback(dataConverter.Unpack<RES>(dataConverter.FromStringRecieve(res)));
#if RELEASE
                }
                catch (System.Exception)
                {
                    LogError("Failed to recieve message", "Cannot convert from raw");
                }
#endif
            });
        }
        protected void Emit(string act, T j, System.Action<T> callback = null, string logMessage = "")
        {
            if (logMessage.Length > 0)
            {
                LogShow(logMessage);
            }
            
            dataConverter.ApplyCredentials(ref j, config.token);
            string data = dataConverter.GoString(j);
            //UnityEngine.Debug.Log("DATA: " + data);
            EmitBase(System.String.Format(
                "{0}{1}{2}",
                prefix, addressSeparator, act
                ), data, (res) => {
                    if (callback != null)
                    {
                        if (logMessage.Length > 0)
                        {
                            LogHide(logMessage);
                        }
                        callback(dataConverter.FromString(res));
                    }
                });
        }
        protected void Emit<REQ, RES>(
            string act,
            REQ req,
            System.Action<RES> onSuccess,
            System.Action<T> onFail,
            System.Action<T> onRecieve = null,
            string logMessage = ""
            )
            where REQ : class
            where RES : class
        {
            T j = null;
            try {
                j = dataConverter.Pack<REQ>(req);
            }
            catch (System.Exception)
            {
                if (onFail != null)
                {
                    onFail(null);
                }
                return;
            }
            
            Emit(act, j, (res) =>
            {
                if (onRecieve != null)
                {
                    onRecieve(res);
                }
                if (IsOk(res))
                {
                    RES r = null;
#if RELEASE
                    try
                    {
#endif
                    r = dataConverter.Unpack<RES>(res);
#if RELEASE
                    }
                    catch (System.Exception)
                    {
                        if (onFail != null)
                        {
                            onFail(null);
                        }
                        return;
                    }
#endif
                    if (onSuccess != null)
                    {
                        onSuccess(r);
                    }
                }
                else
                {
                    if (onFail != null)
                    {
                        onFail(res);
                    }
                }
            }, logMessage);
        }
        protected void Emit<REQ, RES>(
            string act,
            REQ j,
            Dictionary<string, string> errors,
            System.Action<RES> onSuccess,
            System.Func<T, bool> onFail,
            System.Action<T> onRecieve = null,
            string logMessage = ""
            )
            where REQ : class
            where RES : class
        {
            Emit<REQ, RES>(act, j, onSuccess, (res) => {
                ProcessFail(res, errors, (msg) => {
                    if (onFail != null)
                    {
                        return onFail(res);
                    }
                    return true;
                }, prefix + ":" + act);
            },
            onRecieve, logMessage);
        }
        protected bool IsOk(T msg)
        {
            return GetErrorMessage(msg) == errorEmpty;
        }
        protected string GetErrorMessage(T msg)
        {
            if (msg == null)
            {
                return "cannotReadResult";
            }
            return dataConverter.GetErrorMessage(msg);
        }
        protected void ProcessFail(T msg, Dictionary<string, string> dict, System.Func<string, bool> action, string additionalInfo)
        {
            string res;
            if (msg == null)
            {
                res = "messageProcessFail";
            }
            else
            {
                res = GetErrorMessage(msg);
            }
            if (action != null && !action(res))
            {
                return;
            }
            string message = "Unknown error: [" + additionalInfo + "]: " + res;
            if (res != null && dict.ContainsKey(res))
            {
                message = dict[res];
            }
            else if (res != null && baseErrors.ContainsKey(res))
            {
                message = baseErrors[res];
            }
            LogError(message, additionalInfo + " : " + res);
        }
    }
}