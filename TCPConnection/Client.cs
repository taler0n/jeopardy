﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TCPConnection
{
    public class ClientObject
    {
        public string Id { get; private set; }
        public NetworkStream Stream { get; private set; }
        public string UserName { get; private set; }

        private TcpClient _client;

        private Server _server;

        public ClientObject(TcpClient tcpClient, Server serverObject)
        {
            Id = Guid.NewGuid().ToString();
            _client = tcpClient;
            _server = serverObject;
            serverObject.AddConnection(this, Id);
        }

        public void Process()
        {
            try
            {
                Stream = _client.GetStream();
                string message = GetMessage();
                UserName = message;
                _server.ManageNewName(message, Id);
                
                while (true)
                {
                    try
                    {
                        message = GetMessage();

                        if (message.Length > 0)
                        {
                            _server.ManageSignal(Id);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _server.RemoveConnection(this.Id);
                Close();
            }
        }
        
        private string GetMessage()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }
        
        public void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (_client != null)
                _client.Close();
        }
    }
}