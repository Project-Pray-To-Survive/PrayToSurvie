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
        private static StreamWriter _currentWriter;

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
            while (Opened)
            {
                using var server = new NamedPipeServerStream("qss_pray_to_survive", PipeDirection.InOut);
                if (server.IsConnected) server.Disconnect();
                server.WaitForConnection();
                using var reader = new StreamReader(server);
                using var writer = new StreamWriter(server);
                _currentWriter = writer;
                while (Opened)
                {
                    if (reader.EndOfStream) break;
                    var clientMsg = reader.ReadLine();
                    Debug.Log($"IPC Received: {clientMsg}");
                    OnMessageReceived?.Invoke(clientMsg);
                }
                _currentWriter = null;
            }
        }

        public void SendToPipe(string message)
        {
            if (_currentWriter is { BaseStream: { CanWrite: true } })
            {
                _currentWriter.WriteLine(message);
                _currentWriter.Flush();
                Debug.Log($"IPC Sent: {message}");
            }
            else Debug.LogWarning("No client connected to send message.");
        }
    }
}