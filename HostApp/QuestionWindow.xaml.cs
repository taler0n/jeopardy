using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Game;
using TCPConnection;

namespace HostApp
{
    /// <summary>
    /// Логика взаимодействия для QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        private Server _server;
        private Question _question;
        private int _questionValue;
        private Dictionary<string, Player> _players;
        private Queue<string> _answerQueue;
        private List<string> _failedPlayers;

        public string NewActivePlayer;

        public QuestionWindow(Server server, Question question, int value, Dictionary<string, Player> players)
        {
            InitializeComponent();
            _server = server;
            _server.SetSignalManager(new Action<string>(PlayerBuzzedIn));
            _question = question;
            _questionValue = value;
            QuestionLabel.Content = _question.QuestionText;
            _players = players;
            NewActivePlayer = String.Empty;
            _answerQueue = new Queue<string>();
            _failedPlayers = new List<string>();

            _server.BroadcastMessage(MessageSigns.QuestionSign + _question.QuestionText);
        }

        public void PlayerBuzzedIn(string id)
        {
            if (!_failedPlayers.Contains(id))
            {
                _answerQueue.Enqueue(id);
                AnswerQueueBox.Dispatcher.Invoke(new Action<string>(AddNameToList), _players[id].Name);
                RightAnswerButton.Dispatcher.Invoke(new Action<Button>(ShowButton), RightAnswerButton);
                WrongAnswerButton.Dispatcher.Invoke(new Action<Button>(ShowButton), WrongAnswerButton);
            }
        }

        private void AddNameToList(string name)
        {
            AnswerQueueBox.Items.Add(name);
        }

        private void ShowButton(Button button)
        {
            button.Visibility = Visibility.Visible;
        }

        private void RightAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string id = _answerQueue.Dequeue();
            AnswerQueueBox.Items.RemoveAt(0);
            _players[id].Score += _questionValue;
            NewActivePlayer = id;
            ShowAnswer();
            _server.SendMessage(MessageSigns.RightAnswerSign + _questionValue.ToString(), id);
        }

        private void WrongAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string id = _answerQueue.Dequeue();
            AnswerQueueBox.Items.RemoveAt(0);
            _players[id].Score -= _questionValue;
            _failedPlayers.Add(id);

            if (_answerQueue.Count == 0)
            {
                RightAnswerButton.Visibility = Visibility.Hidden;
                WrongAnswerButton.Visibility = Visibility.Hidden;
            }

            _server.SendMessage(MessageSigns.WrongAnswerSign + _questionValue.ToString(), id);
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAnswer();
            _server.BroadcastMessage(MessageSigns.TimesUpMessage);
        }

        private void ShowAnswer()
        {
            QuestionLabel.Content = _question.AnswerText;
            SkipButton.Content = "Выйти";
            SkipButton.RemoveHandler(Button.ClickEvent, (RoutedEventHandler)SkipButton_Click);
            SkipButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(ExitButton_Click));
            RightAnswerButton.Visibility = Visibility.Hidden;
            WrongAnswerButton.Visibility = Visibility.Hidden;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _server.SetSignalManager(null);
        }
    }
}
