using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPConnection
{
    public class Server
    {
        private static TcpListener _listener;
        private Dictionary<string, ClientObject> clients = new Dictionary<string, ClientObject>();
        private Action<string, string> _newNameManagement;
        private Action<string> _removeNameManagement;
        private Action<string> _signalManagement;
        private Action<string, string> _messageManagement;

        public Server(Action<string, string> newPlayerNameManagement, Action<string> removePlayerNameManagement)
        {
            _newNameManagement = newPlayerNameManagement;
            _removeNameManagement = removePlayerNameManagement;
            _signalManagement = null;
            _messageManagement = null;
        }

        public void AddConnection(ClientObject clientObject, string id)
        {
            clients.Add(id, clientObject);
        }

        public void RemoveConnection(string id)
        {
            clients.Remove(id);
            _removeNameManagement(id);
        }

        public void SetSignalManager(Action<string> answerSignalManagement)
        {
            _signalManagement = answerSignalManagement;
        }

        public void SetMessageManager(Action<string, string> messageManagement)
        {
            _messageManagement = messageManagement;
        }

        public void Listen()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, 8888);
                _listener.Start();

                while (true)
                {
                    TcpClient tcpClient = _listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        public string GetLocalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new InvalidOperationException("Нет сети");
        }

        public void ManageNewName(string name, string id)
        {
            _newNameManagement(name, id);
        }

        public void ManageSignal(string id)
        {
            if (_signalManagement != null)
            {
                _signalManagement(id);
            }
        }

        public void ManageMessage(string id, string message)
        {
            if (_messageManagement != null)
            {
                _messageManagement(id, message);
            }
        }

        public void BroadcastMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);

            foreach (var client in clients)
            {
                client.Value.Stream.Write(data, 0, data.Length);
            }
        }

        public void SendMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            clients[id].Stream.Write(data, 0, data.Length);
        }

        public void Disconnect()
        {
            _listener.Stop();

            foreach (var client in clients)
            {
                client.Value.Close();
            }
        }
    }
}
