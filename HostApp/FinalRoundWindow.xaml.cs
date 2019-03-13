using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Game;
using TCPConnection;

namespace HostApp
{
    /// <summary>
    /// Логика взаимодействия для FinalRoundWindow.xaml
    /// </summary>
    public partial class FinalRoundWindow : Window
    {
        private const string _styleName = "YellowContentStyle";

        private Server _server;
        private FinalRound _final;
        private Settings _settings;
        private Dictionary<string, Player> _players;
        private Dictionary<string, int> _bets;
        private bool _canStart;
        private List<Button> _questionButtons;

        public FinalRoundWindow(Server server, FinalRound final, Settings settings, Dictionary<string, Player> players)
        {
            InitializeComponent();
            _server = server;
            _server.SetMessageManager(new Action<string, string>(ManageBet));
            _final = final;
            _settings = settings;
            _players = players;
            _bets = new Dictionary<string, int>();
            _canStart = false;
            _questionButtons = new List<Button>();
            DrawQuestionGrid();
            _server.BroadcastMessage(MessageSigns.FinalRoundMessage);
        }

        public void ManageBet(string id, string message)
        {
            int value;

            if (int.TryParse(message, out value))
            {
                if (!_bets.ContainsKey(id))
                {
                    if (value >= 0 && value <= _players[id].Score)
                    {
                        _bets.Add(id, value);
                    }
                }

                if (_bets.Count == _players.Count)
                {
                    _canStart = true;
                }
            }
        }

        private void DrawQuestionGrid()
        {
            for (int i = 0; i < _settings.FinalSize.Value; i++)
            {
                RowDefinition row = new RowDefinition();
                QuestionGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < _settings.FinalSize.Value; i++)
            {
                Button question = new Button();
                question.Name = String.Format("QuestionButton_{0}", i);
                question.Style = (Style)FindResource(_styleName);
                question.Content = _final[i].Name;
                Grid.SetRow(question, i);
                Grid.SetColumn(question, 1);
                question.AddHandler(Button.ClickEvent, new RoutedEventHandler(QuestionButton_Click));
                _questionButtons.Add(question);
                QuestionGrid.Children.Add(question);
            }
        }

        private void QuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_canStart)
            {
                ((Button)sender).Visibility = Visibility.Hidden;

                int buttonsAvailable = 0;
                FinalQuestion lastQuestion = null;

                for (int i = 0; i < _questionButtons.Count; i++)
                {
                    if (_questionButtons[i].Visibility == Visibility.Visible)
                    {
                        buttonsAvailable++;
                        lastQuestion = _final[i];
                    }
                }

                if (buttonsAvailable == 1)
                {
                    var finalQuestionWindow = new FinalQuestionWindow(_server, lastQuestion, _settings, _players, _bets);
                    Visibility = Visibility.Hidden;
                    finalQuestionWindow.ShowDialog();
                    Close();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _server.SetMessageManager(null);
        }
    }
}
