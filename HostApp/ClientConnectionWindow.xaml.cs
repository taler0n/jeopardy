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
using TCPConnection;
using Game;


namespace HostApp
{
    /// <summary>
    /// Логика взаимодействия для ClientConnectionWindow.xaml
    /// </summary>
    public partial class ClientConnectionWindow : Window
    {
        private Server _server;
        private Thread _listenThread;
        private Dictionary<string, Player> _players;

        public ClientConnectionWindow()
        {
            InitializeComponent();
            var newNameHandler = new Action<string, string>(AddPlayer);
            var buzzInHandler = new Action<string>(BuzzIn);
            var removeNameHandler = new Action<string>(RemoveName);
            _server = new Server(newNameHandler, buzzInHandler, removeNameHandler);
            _listenThread = new Thread(new ThreadStart(_server.Listen));
            IPOutput.Content = _server.GetLocalIP();
            _players = new Dictionary<string, Player>();

            try
            {
                _listenThread.Start();
            }
            catch (Exception ex)
            {
                _server.Disconnect();
                MessageBox.Show(ex.Message);
            }
        }

        private void AddPlayer(string name, string id)
        {
            _players[id] = new Player(name);
            PrintNames();
        }

        private void PrintNames()
        {
            NamesOutput.Clear();

            foreach (var player in _players)
            {
                NamesOutput.Text += String.Format("{0}\n", player.Value.Name);
            }
        }

        private void BuzzIn(string id)
        {
            BuzzerLabel.Content = String.Format("!!{0}!!", _players[id].Name);
        }

        private void RemoveName(string id)
        {
            _players.Remove(id);
            PrintNames();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _server.Disconnect();
        }
    }
}
