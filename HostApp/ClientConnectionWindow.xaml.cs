using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
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
        private GameObject _game;
        private bool _gameStarted;

        public ClientConnectionWindow(GameObject newGame)
        {
            InitializeComponent();
            PlayersList.DisplayMemberPath = "Value";
            _players = new Dictionary<string, Player>();
            _game = newGame;
            _gameStarted = false;

            var newNameHandler = new Action<string, string>(AddPlayer);
            var removeNameHandler = new Action<string>(RemoveName);
            _server = new Server(newNameHandler, removeNameHandler);
            _listenThread = new Thread(new ThreadStart(_server.Listen));
            IPOutput.Content = _server.GetLocalIP();

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
            if (_gameStarted)
            {
                _server.RemoveConnection(id);
            }
            else
            {
                _players[id] = new Player(name);
                PlayersList.Dispatcher.Invoke(new Action<KeyValuePair<string, string>>(AddNameToList), new KeyValuePair<string, string>(id, name));
            }
        }

        private void AddNameToList(KeyValuePair<string, string> pair)
        {
            PlayersList.Items.Add(pair);
        }

        private void RemoveName(string id)
        {
            if (_players.ContainsKey(id))
            {
                PlayersList.Dispatcher.Invoke(new Action<KeyValuePair<string, string>>(RemoveNameFromList), new KeyValuePair<string, string>(id, _players[id].Name));
                _players.Remove(id);
            }

        }

        private void RemoveNameFromList(KeyValuePair<string, string> pair)
        {
            PlayersList.Items.Remove(pair);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _server.Disconnect();
        }

        private void KickPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayersList.SelectedItem != null)
            {
                var item = PlayersList.SelectedItem;
                string id = ((KeyValuePair<string, string>)item).Key;
                _server.SendMessage(MessageSigns.DisconnectMessage, id);
                _server.RemoveConnection(id);
            }
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (_players.Count == _game.Settings.NumberOfPlayers.Value)
            {
                _gameStarted = true;
                Visibility = Visibility.Hidden;

                foreach (var round in _game.Rounds)
                {
                    var roundWindow = new GameWindow(_server, round, _game.Settings, _players);
                    roundWindow.ShowDialog();
                }

                var final = new FinalRoundWindow(_server, _game.Final, _game.Settings, _players);
                final.ShowDialog();
                Close();
            }
        }
    }
}
