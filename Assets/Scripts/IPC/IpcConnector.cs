using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace IPC
{
    public class IpcConnector : MonoBehaviour
    {
        private static Socket _server;
        private static bool _isAlive;
        public const int Port = 7777;
        public static event Action<string> OnMessageReceived=_=>{};
        
        private void OnEnable()
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var endPoint = new IPEndPoint(ipAddress, Port);
 
            _server = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _server.Bind(endPoint); 
                _server.Listen(1);
                _isAlive = true;

                new Thread(Listen).Start();
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
            Debug.Log("IPC Server started: " + _server);
        }

        private static void Listen()
        {
            var clientSocket = _server.Accept();
            while (_isAlive)
            {
                var receiveBuffer = new byte[1024];
                var receiveBytes = clientSocket.Receive(receiveBuffer);
                if (receiveBytes <= 0)
                {
                    clientSocket = _server.Accept();
                    continue;
                }
                var receiveData = Encoding.UTF8.GetString(receiveBuffer, 0, receiveBytes);
                OnMessageReceived.Invoke(receiveData);
                Debug.Log($"클라이언트로부터 받음: {receiveData}");
            }
        }

        private void OnDisable()
        {
            _isAlive = false;
            if (_server == null) return;
            _server.Close();
            _server.Dispose();
            _server = null;
        }
    }
}
