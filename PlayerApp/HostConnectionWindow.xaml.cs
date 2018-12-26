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
using Game;

namespace PlayerApp
{
    /// <summary>
    /// Логика взаимодействия для HostConnectionWindow.xaml
    /// </summary>
    public partial class HostConnectionWindow : Window
    {
        private const string _ipPlaceholder = "Введите IP адрес...";
        private const string _namePlaceholder = "Введите имя...";
        private const int port = 8888;

        public TcpClient Client;
        public NetworkStream Stream;
        public string UserName;
        
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
            if (!String.IsNullOrWhiteSpace(IPInput.Text) && !String.IsNullOrWhiteSpace(NameInput.Text) && (NameInput.Text.Length <= Player.MaxNameLength))
            {
                UserName = NameInput.Text;
                string host = IPInput.Text;
                
                try
                {
                    Client = new TcpClient();
                    Client.Connect(host, port);
                    Stream = Client.GetStream();
                    byte[] data = Encoding.Unicode.GetBytes(UserName);
                    Stream.Write(data, 0, data.Length);
                    Close();
                }
                catch
                {
                    IPInput.Text = _ipPlaceholder;
                    NameInput.Text = _namePlaceholder;
                }
            }
            else
            {
                IPInput.Text = _ipPlaceholder;
                NameInput.Text = _namePlaceholder;
            }
        }
    }
}
