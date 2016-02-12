using System.Collections.Generic;

namespace ACO.Net.Samples.SocketIO.JN
{
    public class Bridge : Protocol.SocketIO.Bridge<JSONObject>
    {
        public Bridge(DataFormat.JN.Converter dataConverter, Config config, Net.Protocol.SocketIO.Connector connector) : base(dataConverter, config, connector)
        {
            prefix = "bridge";
        }
        private static readonly Dictionary<string, string> testErrors = new Dictionary<string, string>
        {
        };
        public void Test(Request req,
            System.Action<Response> onSuccess = null,
            System.Func<JSONObject, bool> onFail = null
            )
        {
            Emit("test", Pack(req), testErrors, (res) => {
                onSuccess(Unpack<Response>(res));
            }, (res) => {
                return onFail(new JSONObject());
            }, null, "Testing bridge");
            //gift.sender = Core.get.userProfile.cloudUserId;
            //JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
            //j.AddField("gift", gift.ToJSON());
            //Emit("send", j, sendErrors, onSuccess, onFail, null, "Sending gift");
        }
    }
}