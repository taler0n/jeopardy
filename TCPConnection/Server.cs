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
        private Action<string> _signalManagement;
        private Action<string> _removeNameManagement;

        public Server(Action<string, string> newPlayerNameManagement, Action<string> answerSignalManagement, Action<string> removePlayerNameManagement)
        {
            _newNameManagement = newPlayerNameManagement;
            _signalManagement = answerSignalManagement;
            _removeNameManagement = removePlayerNameManagement;
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

            throw new InvalidOperationException("No local network");
        }

        public void ManageNewName(string name, string id)
        {
            _newNameManagement(name, id);
        }

        public void ManageSignal(string id)
        {
            _signalManagement(id);
        }

        public void BroadcastMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);

            foreach (var client in clients)
            {
                client.Value.Stream.Write(data, 0, data.Length);
            }
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
