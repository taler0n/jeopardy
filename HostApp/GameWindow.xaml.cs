using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Game;
using TCPConnection;

namespace HostApp
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private const string _styleName = "YellowContentStyle";

        private Server _server;
        private Round _round;
        private Settings _settings;
        private Dictionary<string, Player> _players;
        private Dictionary<string, Label> _nameLabels;
        private Dictionary<string, TextBox> _scoreBoxes;
        private string _activePlayer;
        private int _questionsAnswered;

        public GameWindow(Server server, Round round, Settings settings, Dictionary<string, Player> players)
        {
            InitializeComponent();
            _server = server;
            _round = round;
            _settings = settings;
            _players = players;
            _nameLabels = new Dictionary<string, Label>();
            _scoreBoxes = new Dictionary<string, TextBox>();
            _activePlayer = _players.Keys.First();
            _questionsAnswered = 0;

            foreach (var player in _players)
            {
                if (player.Value.Score < _players[_activePlayer].Score)
                {
                    _activePlayer = player.Key;
                }
            }

            DrawPlayersGrid();
            DrawQuestionGrid();
            UpdatePlayersInfo();
        }

        private void DrawPlayersGrid()
        {
            ColumnDefinition column0 = new ColumnDefinition();
            column0.Width = new GridLength(1, GridUnitType.Star);
            PlayersGrid.ColumnDefinitions.Add(column0);

            for (int i = 0; i < _settings.NumberOfPlayers.Value; i++)
            {
                ColumnDefinition playerColumn = new ColumnDefinition();
                playerColumn.Width = new GridLength(4, GridUnitType.Star);
                PlayersGrid.ColumnDefinitions.Add(playerColumn);

                ColumnDefinition divisionColumn = new ColumnDefinition();
                divisionColumn.Width = new GridLength(1, GridUnitType.Star);
                PlayersGrid.ColumnDefinitions.Add(divisionColumn);
            }

            int columnIndex = 1;
            foreach (var pair in _players)
            {
                var player = pair.Value;

                Label playerName = new Label();
                playerName.Style = (Style)FindResource(_styleName);
                playerName.Content = player.Name;
                Grid.SetColumn(playerName, columnIndex);
                Grid.SetRow(playerName, 0);
                PlayersGrid.Children.Add(playerName);
                _nameLabels.Add(pair.Key, playerName);

                TextBox playerScore = new TextBox();
                playerScore.Style = (Style)FindResource(_styleName);
                playerScore.Text = player.Score.ToString();
                playerScore.IsReadOnly = true;
                Grid.SetColumn(playerScore, columnIndex);
                Grid.SetRow(playerScore, 1);
                PlayersGrid.Children.Add(playerScore);
                _scoreBoxes.Add(pair.Key, playerScore);

                columnIndex += 2;
            }
        }

        private void DrawQuestionGrid()
        {
            for (int i = 0; i < _settings.RoundSize.Value; i++)
            {
                RowDefinition row = new RowDefinition();
                QuestionGrid.RowDefinitions.Add(row);
            }

            ColumnDefinition namesColumn = new ColumnDefinition();
            namesColumn.Width = new GridLength(2, GridUnitType.Star);
            QuestionGrid.ColumnDefinitions.Add(namesColumn);

            for (int i = 0; i < _settings.ThemeSize.Value; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                QuestionGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < _settings.RoundSize.Value; i++)
            {
                Label themeName = new Label();
                themeName.Style = (Style)FindResource(_styleName);
                themeName.Content = _round[i].Name;
                Grid.SetColumn(themeName, 0);
                Grid.SetRow(themeName, i);
                QuestionGrid.Children.Add(themeName);
            }

            for (int i = 0; i < _settings.RoundSize.Value; i++)
            {
                for (int j = 0; j < _settings.ThemeSize.Value; j++)
                {
                    Button question = new Button();
                    question.Name = String.Format("QuestionButton_{0}_{1}", i, j);
                    question.Style = (Style)FindResource(_styleName);
                    question.Content = _round[i].GetQuestionValue(j);
                    Grid.SetColumn(question, j + 1);
                    Grid.SetRow(question, i);
                    question.AddHandler(Button.ClickEvent, new RoutedEventHandler(QuestionButton_Click));
                    QuestionGrid.Children.Add(question);
                }
            }
        }

        private void QuestionButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] words = button.Name.Split('_');
            int themeNumber = int.Parse(words[1]);
            int questionNumber = int.Parse(words[2]);
            var theme = _round[themeNumber];
            int value = theme.GetQuestionValue(questionNumber);

            var questionWindow = new QuestionWindow(_server, theme[questionNumber], value, _players);
            questionWindow.ShowDialog();
            button.Visibility = Visibility.Hidden;
            _questionsAnswered++;

            if (questionWindow.NewActivePlayer != String.Empty)
            {
                _activePlayer = questionWindow.NewActivePlayer;
            }

            if (_questionsAnswered == _settings.RoundSize.Value * _settings.ThemeSize.Value)
            {
                Close();
            }

            UpdatePlayersInfo();
        }

        private void UpdatePlayersInfo()
        {
            foreach (var pair in _scoreBoxes)
            {
                pair.Value.Text = _players[pair.Key].Score.ToString();
            }

            foreach (var pair in _nameLabels)
            {
                if (pair.Key == _activePlayer)
                {
                    pair.Value.Foreground = Brushes.Red;
                }
                else
                {
                    pair.Value.Foreground = Brushes.Yellow;
                }
            }
        }
        
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            bool correctChanges = true;
            var newValues = new Queue<int>();

            foreach (var pair in _players)
            {
                int newValue;
                if (int.TryParse(_scoreBoxes[pair.Key].Text, out newValue))
                {
                    newValues.Enqueue(newValue);
                }
                else
                {
                    correctChanges = false;
                    break;
                }
            }

            if (correctChanges)
            {
                foreach (var pair in _players)
                {
                    int newValue = newValues.Dequeue();
                    int delta = newValue - pair.Value.Score;
                    pair.Value.Score = newValue;

                    if (delta < 0)
                    {
                        _server.SendMessage(MessageSigns.WrongAnswerSign + (-delta).ToString(), pair.Key);
                    }
                    else
                    {
                        _server.SendMessage(MessageSigns.RightAnswerSign + delta, pair.Key);
                    }
                }

                EditButton.Visibility = Visibility.Visible;
                SaveChangesButton.Visibility = Visibility.Hidden;
                SkipRoundButton.Visibility = Visibility.Hidden;

                foreach (var box in _scoreBoxes.Values)
                {
                    box.IsReadOnly = true;
                    box.Foreground = Brushes.Yellow;
                }
            }
        }

        private void SkipRoundButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditButton.Visibility = Visibility.Hidden;
            SaveChangesButton.Visibility = Visibility.Visible;
            SkipRoundButton.Visibility = Visibility.Visible;

            foreach (var box in _scoreBoxes.Values)
            {
                box.IsReadOnly = false;
                box.Foreground = Brushes.White;
            }
        }
    }
}
