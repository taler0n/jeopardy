using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Sockets;
using TCPConnection;

namespace PlayerApp
{
    /// <summary>
    /// Логика взаимодействия для HostConnectionWindow.xaml
    /// </summary>
    public partial class HostConnectionWindow : Window
    {
        private const string _ipPlaceholder = "Enter IP here...";
        private const string _namePlaceholder = "Enter your name here...";
        private const string _defaultSignal = "!";

        private static TcpClient client;
        private static NetworkStream stream;
        private const int port = 8888;

        public HostConnectionWindow()
        {
            InitializeComponent();
            IPInput.Text = _ipPlaceholder;
            NameInput.Text = _namePlaceholder;
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = String.Empty;
        }

        private void Input_LostFocus(object sender, RoutedEventArgs e)
        {
            var box = (TextBox)sender;

            if (String.IsNullOrWhiteSpace(box.Text))
            {
                string placeholder = String.Empty;
                switch (box.Name)
                {
                    case "IPInput":
                        {
                            placeholder = _ipPlaceholder;
                            break;
                        }
                    case "NameInput":
                        {
                            placeholder = _namePlaceholder;
                            break;
                        }
                    default:
                        break;
                }
                box.Text = placeholder;
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(IPInput.Text) && !String.IsNullOrWhiteSpace(NameInput.Text))
            {
                string userName = NameInput.Text;
                string host = IPInput.Text;
                client = new TcpClient();

                try
                {
                    client.Connect(host, port);
                    stream = client.GetStream();
                    byte[] data = Encoding.Unicode.GetBytes(userName);
                    stream.Write(data, 0, data.Length);

                    //Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                    //receiveThread.Start();
                }
                finally
                { 

                }
            }
        }
        
        private void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }

        private void BuzzButton_Click(object sender, RoutedEventArgs e)
        {
            if (stream != null && client != null)
            {
                byte[] data = Encoding.Unicode.GetBytes(_defaultSignal);
                stream.Write(data, 0, data.Length);
            }
            else
            {
                MessageBox.Show("No connection");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Disconnect();
        }
    }
}
