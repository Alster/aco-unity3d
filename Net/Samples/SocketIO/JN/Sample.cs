using UnityEngine;

namespace ACO.Net.Samples.SocketIO.JN
{
    public class Sample : MonoBehaviour
    {
        public Protocol.SocketIO.Connector connector;
        public Bridge bridge;

        void Start()
        {
            connector.Connect();

            DataFormat.JN.Converter converter = new DataFormat.JN.Converter();
            Config config = new Config();
            Credentials credentials = new Credentials();
            credentials.uid = "someuid";
            credentials.token = "sometoken";
            config.credentials = credentials;
            config.logBegin = Debug.Log;
            config.logFinish = (m) => { };
            config.errorRaise = Debug.LogError;
            bridge = new Bridge(converter, config, connector);
        }

        public void Run()
        {
            Request req = new Request();
            req.someQuestion = "question, lol";
            bridge.Test(req, (r) =>
            {
                Debug.Log("Success: " + r.someAnswer);
            }, (r) =>
            {
                Debug.Log("Fail");
                return false;
            });
        }
    }
}