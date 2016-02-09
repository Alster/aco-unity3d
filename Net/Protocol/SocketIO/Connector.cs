using UnityEngine;
using SocketIO;

namespace ACO.Net.Protocol.SocketIO
{
    public class Connector : ACO.Net.Protocol.Connector
    {
        public string IP = "localhost";
        public int PORT = 4567;

        public bool opened = false;
        public bool isErrorRecieved  = false;

        public SocketIOComponent socketPrefab;
        public SocketIOComponent socket { get; private set; }

        public ACO.Event onOpen;

        public bool isConnected
        {
            get
            {
                return socket != null ? socket.IsConnected : false;
            }
        }
        public void Connect() {
            Connect(IP, PORT);
        }
        public void Connect(string ip)
        {
            Connect(ip, PORT);
        }
        public void Connect(string ip, int port)
        {
            if (isConnected)
            {
                Debug.LogWarning("Can't connect socket. Socket is already connected");
                return;
            }

            socket = Instantiate(socketPrefab) as SocketIOComponent;
            socket.transform.SetParent(transform);
            socket.url = string.Format("ws://{0}:{1}/socket.io/?EIO=4&transport=websocket", ip, port);
            socket.Initialize();

            socket.Connect();
            
            socket.On("open", OnOpen);
            socket.On("error", OnError);
            socket.On("close", OnClose);
        }
        public void Subscribe(string address, System.Action<string> callback)
        {
            socket.On(address, (res) => {
                callback(res.data != null ? res.data.str : "");
            });
        }
        public void Close()
        {
            opened = false;
            socket.Close();
            Destroy(socket.gameObject);
            socket = null;
        }

        void Start()
        {
        }
        void OnOpen(SocketIOEvent e)
        {
            if (opened)
            {
                return;
            }
            opened = true;
            isErrorRecieved = false;
            Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data, this);
            
            onOpen.Invoke();
        }
        void OnClose(SocketIOEvent e)
        {
            opened = false;
            Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data, this);
        }
        void OnError(SocketIOEvent e)
        {
            opened = false;
            Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data, this);
            if (!isErrorRecieved)
            {
                isErrorRecieved = true;
            }
        }
    }
}