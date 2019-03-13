using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Game;
using TCPConnection;

namespace HostApp
{
    /// <summary>
    /// Логика взаимодействия для FinalQuestionWindow.xaml
    /// </summary>
    public partial class FinalQuestionWindow : Window
    {
        private class PlayerHUD
        {
            private const string _styleName = "YellowContentStyle";
            private const string _rightAnswerButtonText = "+";
            private const string _wrongAnswerButtonText = "-";
            private const int _answerButtonWidth = 3;

            public Label NameLabel;
            public Label ScoreLabel;
            public Label AnswerLabel;
            public Button RightAnswerButton;
            public Button WrongAnswerButton;
            public bool Checked;

            public PlayerHUD(Player player, string id, Grid grid, int columnIndex, FinalQuestionWindow window, Dictionary<Button, string> idFromButtons)
            {
                Checked = false;

                NameLabel = new Label();
                NameLabel.Style = (Style)window.FindResource(_styleName);
                NameLabel.Content = player.Name;
                Grid.SetColumn(NameLabel, columnIndex);
                Grid.SetRow(NameLabel, 0);
                grid.Children.Add(NameLabel);

                ScoreLabel = new Label();
                ScoreLabel.Style = (Style)window.FindResource(_styleName);
                ScoreLabel.Content = player.Score.ToString();
                Grid.SetColumn(ScoreLabel, columnIndex);
                Grid.SetRow(ScoreLabel, 1);
                grid.Children.Add(ScoreLabel);

                AnswerLabel = new Label();
                AnswerLabel.Style = (Style)window.FindResource(_styleName);
                AnswerLabel.Visibility = Visibility.Hidden;
                Grid.SetColumn(AnswerLabel, columnIndex);
                Grid.SetRow(AnswerLabel, 2);
                grid.Children.Add(AnswerLabel);

                Grid buttonGrid = new Grid();
                ColumnDefinition column0 = new ColumnDefinition();
                column0.Width = new GridLength(_answerButtonWidth, GridUnitType.Star);
                buttonGrid.ColumnDefinitions.Add(column0);
                ColumnDefinition column1 = new ColumnDefinition();
                column1.Width = new GridLength(1, GridUnitType.Star);
                buttonGrid.ColumnDefinitions.Add(column1);
                ColumnDefinition column2 = new ColumnDefinition();
                column2.Width = new GridLength(_answerButtonWidth, GridUnitType.Star);
                buttonGrid.ColumnDefinitions.Add(column2);
                Grid.SetColumn(buttonGrid, columnIndex);
                Grid.SetRow(buttonGrid, 3);
                grid.Children.Add(buttonGrid);
                
                RightAnswerButton = new Button();
                RightAnswerButton.Style = (Style)window.FindResource(_styleName);
                RightAnswerButton.Content = _rightAnswerButtonText;
                RightAnswerButton.Visibility = Visibility.Hidden;
                Grid.SetColumn(RightAnswerButton, 0);
                RightAnswerButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(window.RightAnswerButton_Click));
                buttonGrid.Children.Add(RightAnswerButton);
                idFromButtons.Add(RightAnswerButton, id);

                WrongAnswerButton = new Button();
                WrongAnswerButton.Style = (Style)window.FindResource(_styleName);
                WrongAnswerButton.Content = _wrongAnswerButtonText;
                WrongAnswerButton.Visibility = Visibility.Hidden;
                Grid.SetColumn(WrongAnswerButton, 2);
                WrongAnswerButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(window.WrongAnswerButton_Click));
                buttonGrid.Children.Add(WrongAnswerButton);
                idFromButtons.Add(WrongAnswerButton, id);
            }

            public void Update(int finalScore)
            {
                ScoreLabel.Content = finalScore.ToString();
                RightAnswerButton.Visibility = Visibility.Hidden;
                WrongAnswerButton.Visibility = Visibility.Hidden;
                Checked = true;
            }
        }

        private const int _playerColumnWidth = 4;
        private const int _divisionColumnWidth = 1;
        private const string _endGame = "Игра окончена";

        private Server _server;
        private FinalQuestion _question;
        private Settings _settings;
        private Dictionary<string, Player> _players;
        private Dictionary<string, int> _bets;
        private Dictionary<string, string> _answers;
        private Dictionary<string, PlayerHUD> _huds;
        private Dictionary<Button, string> _idFromButtons;

        public FinalQuestionWindow(Server server, FinalQuestion question, Settings settings, Dictionary<string, Player> players, Dictionary<string, int> bets)
        {
            InitializeComponent();
            _server = server;
            _server.SetMessageManager(new Action<string, string>(ManageAnswer));
            _question = question;
            _settings = settings;
            _players = players;
            _bets = bets;
            _answers = new Dictionary<string, string>();
            _huds = new Dictionary<string, PlayerHUD>();
            _idFromButtons = new Dictionary<Button, string>();
            DrawPlayersGrid();
            _server.BroadcastMessage(MessageSigns.QuestionSign + _question.QuestionText);
        }

        public void ManageAnswer(string id, string message)
        {
            if (!_answers.ContainsKey(id))
            {
                _answers.Add(id, message);
            }

            if (_answers.Count == _players.Count)
            {
                ShowAnswersButton.Dispatcher.Invoke(new Action<Button>(ShowButton), ShowAnswersButton);
            }
        }

        private void ShowButton(Button button)
        {
            button.Visibility = Visibility.Visible;
        }

        private void DrawPlayersGrid()
        {
            ColumnDefinition column0 = new ColumnDefinition();
            column0.Width = new GridLength(_divisionColumnWidth, GridUnitType.Star);
            PlayersGrid.ColumnDefinitions.Add(column0);

            for (int i = 0; i < _settings.NumberOfPlayers.Value; i++)
            {
                ColumnDefinition playerColumn = new ColumnDefinition();
                playerColumn.Width = new GridLength(_playerColumnWidth, GridUnitType.Star);
                PlayersGrid.ColumnDefinitions.Add(playerColumn);

                ColumnDefinition divisionColumn = new ColumnDefinition();
                divisionColumn.Width = new GridLength(_divisionColumnWidth, GridUnitType.Star);
                PlayersGrid.ColumnDefinitions.Add(divisionColumn);
            }

            int columnIndex = 1;
            foreach (var pair in _players)
            {
                var newHUD = new PlayerHUD(pair.Value, pair.Key, PlayersGrid, columnIndex, this, _idFromButtons);
                _huds.Add(pair.Key, newHUD);
                columnIndex += 2;
            }
        }

        public void RightAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string id = _idFromButtons[(Button)sender];
            _players[id].Score += _bets[id];
            _server.SendMessage(MessageSigns.RightAnswerSign + _bets[id].ToString(), id);
            _huds[id].Update(_players[id].Score);
            CheckHUDs();
        }

        public void WrongAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string id = _idFromButtons[(Button)sender];
            _players[id].Score -= _bets[id];
            _server.SendMessage(MessageSigns.WrongAnswerSign + _bets[id].ToString(), id);
            _huds[id].Update(_players[id].Score);
            CheckHUDs();
        }

        private void CheckHUDs()
        {
            int count = 0;

            foreach (var hud in _huds.Values)
            {
                if (hud.Checked)
                {
                    count++;
                }
            }

            if (count == _huds.Count)
            {
                QuestionLabel.Content = _endGame;
            }
        }

        private void ShowAnswersButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionLabel.Content = _question.AnswerText;
            ShowAnswersButton.Visibility = Visibility.Hidden;

            foreach (var hud in _huds)
            {
                hud.Value.AnswerLabel.Content = _answers[hud.Key];
                hud.Value.AnswerLabel.Visibility = Visibility.Visible;
                hud.Value.RightAnswerButton.Visibility = Visibility.Visible;
                hud.Value.WrongAnswerButton.Visibility = Visibility.Visible;
            }
        }
    }
}
