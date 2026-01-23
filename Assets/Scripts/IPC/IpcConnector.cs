using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using UnityEngine;

namespace IPC
{
    public class IpcConnector : MonoBehaviour
    {
        public static bool Opened;
        public static event Action<string> OnMessageReceived = _ => { };

        private void OnEnable()
        {
            try
            {
                Opened = true;
                new Thread(Listen).Start();
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        private void OnDisable() => Opened = false;

        private static void Listen()
        {
            s:
            while (Opened)
            {
                using var server = new NamedPipeServerStream("qss_pray_to_survive", PipeDirection.InOut);
                if (server.IsConnected) server.Disconnect();
                server.WaitForConnection();
                using var reader = new StreamReader(server);
                while (Opened)
                {
                    if (reader.EndOfStream) goto s;
                    var clientMsg = reader.ReadLine();
                    Debug.Log($"IPC Received: {clientMsg}");
                    OnMessageReceived?.Invoke(clientMsg);
                }
            }
        }
    }
}
