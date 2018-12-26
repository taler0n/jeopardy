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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using Game;
using TCPConnection;

namespace PlayerApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _questionPlaceholder = "Ожидание вопроса...";
        private const string _connectionPlaceholder = "Ожидание соединения...";
        private const string _noConnectionMessage = "Нет соединения";
        private const string _wrongMessageSign = "Неизвестный тип входящего сообщения";

        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _receiveThread;
        private Player _player;
        private bool _finalStarted;
        private int _bet;

        public MainWindow()
        {
            InitializeComponent();
            MessageTextbox.Text = _connectionPlaceholder;
            _finalStarted = false;
            _bet = 0;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(MessageSigns.DisconnectMessage);
            Disconnect();
            var connectionWindow = new HostConnectionWindow();
            Visibility = Visibility.Hidden;
            connectionWindow.ShowDialog();
            Visibility = Visibility.Visible;
            if (connectionWindow.Client != null && connectionWindow.Stream != null)
            {
                _client = connectionWindow.Client;
                _stream = connectionWindow.Stream;
                _player = new Player(connectionWindow.UserName);
                NameLabel.Content = _player.Name;
                ScoreLabel.Content = _player.Score;
                MessageTextbox.Text = _questionPlaceholder;

                if (_receiveThread != null)
                {
                    _receiveThread.Abort();
                }

                _receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                _receiveThread.Start();
            }
            else
            {
                MessageTextbox.Text = _noConnectionMessage;
            }
        }

        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;

                    do
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (_stream.DataAvailable);

                    if (builder.Length >= MessageSigns.SignLength)
                    {
                        string code = builder.ToString(0, MessageSigns.SignLength);
                        string message = builder.ToString(MessageSigns.SignLength, builder.Length - MessageSigns.SignLength);
                        ProcessMessage(code, message);
                    }
                }
                catch
                {
                    MessageTextbox.Dispatcher.Invoke(new Action<string>(ChangeMessageBox), _noConnectionMessage);
                    Disconnect();
                    break;
                }
            }
        }

        private void ProcessMessage(string code, string message)
        {
            switch (code)
            {
                case MessageSigns.DisconnectMessage:
                    {
                        Disconnect();
                        break;
                    }
                case MessageSigns.QuestionSign:
                    {
                        if (_finalStarted)
                        {
                            Dispatcher.Invoke(new Action<string>(CreateAnswerWindow), message);
                        }
                        else
                        {
                            MessageTextbox.Dispatcher.Invoke(new Action<string>(ChangeMessageBox), message);
                        }
                        break;
                    }
                case MessageSigns.RightAnswerSign:
                    {
                        int increment;
                        if (int.TryParse(message, out increment))
                        {
                            _player.Score += increment;
                            ScoreLabel.Dispatcher.Invoke(new Action<string>(ChangeScoreLabel), _player.Score.ToString());
                        }

                        MessageTextbox.Dispatcher.Invoke(new Action<string>(ChangeMessageBox), _questionPlaceholder);
                        break;
                    }
                case MessageSigns.WrongAnswerSign:
                    {
                        int decrement;
                        if (int.TryParse(message, out decrement))
                        {
                            _player.Score -= decrement;
                            ScoreLabel.Dispatcher.Invoke(new Action<string>(ChangeScoreLabel), _player.Score.ToString());
                        }

                        MessageTextbox.Dispatcher.Invoke(new Action<string>(ChangeMessageBox), _questionPlaceholder);
                        break;
                    }
                case MessageSigns.FinalRoundMessage:
                    {
                        if (_player.Score > 0)
                        {
                            MakeBetWindow betInput = null;
                            Dispatcher.Invoke(new Action<MakeBetWindow>(CreateBetWindow), betInput);
                        }

                        string betMessage = MessageSigns.AnswerMessage + _bet.ToString();
                        SendMessage(betMessage);
                        _finalStarted = true;
                        break;
                    }
                case MessageSigns.TimesUpMessage:
                    {
                        MessageTextbox.Dispatcher.Invoke(new Action<string>(ChangeMessageBox), _questionPlaceholder);
                        break;
                    }
                default:
                    {
                        MessageTextbox.Dispatcher.Invoke(new Action<string>(ChangeMessageBox), _wrongMessageSign);
                        break;
                    }
            }
        }

        private void CreateAnswerWindow(string message)
        {
            var answerWindow = new SendAnswerWindow(_stream, _player, message);
            Visibility = Visibility.Hidden;
            answerWindow.ShowDialog();
            Close();
        }

        private void CreateBetWindow(MakeBetWindow betInput)
        {
            betInput = new MakeBetWindow(_player);
            Visibility = Visibility.Hidden;
            betInput.ShowDialog();
            Visibility = Visibility.Visible;
            _bet = betInput.Bet;
        }

        private void ChangeMessageBox(string text)
        {
            MessageTextbox.Text = text;
        }

        private void ChangeScoreLabel(string text)
        {
            ScoreLabel.Content = text;
        }

        private void Disconnect()
        {
            if (_stream != null)
                _stream.Close();
            if (_client != null)
                _client.Close();
        }

        private void BuzzButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(MessageSigns.BuzzMessage);
        }
        private void SendMessage(string message)
        {
            try
            { 
                byte[] data = Encoding.Unicode.GetBytes(message);
                _stream.Write(data, 0, data.Length);
            }
            catch
            {
                MessageTextbox.Text = _noConnectionMessage;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SendMessage(MessageSigns.DisconnectMessage);
            Disconnect();
        }
    }
}
