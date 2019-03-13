using System;
using System.Windows;
using System.Windows.Controls;
using Game;

namespace PlayerApp
{
    /// <summary>
    /// Логика взаимодействия для MakeBetWindow.xaml
    /// </summary>

    public partial class MakeBetWindow : Window
    {
        private Player _player;
        private const string _betPlaceholder = "Введите размер ставки...";
        private const string _betErrorMessage = "Введите число";
        private const string _betBelowZero = "Ставка должна быть неотрицательным числом";
        private const string _betTooBig = "Ставка не может превышать счет";
        private const int _maxBetLength = 10;

        public int Bet;

        public MakeBetWindow(Player player)
        {
            InitializeComponent();
            Bet = 0;
            _player = player;
            NameLabel.Content = _player.Name;
            ScoreLabel.Content = _player.Score;
            BetInput.Text = _betPlaceholder;
        }

        private void SetBetButton_Click(object sender, RoutedEventArgs e)
        {
            int output;

            if (int.TryParse(BetInput.Text, out output))
            {
                if (output >= 0)
                {
                    if (output <= _player.Score)
                    {
                        Bet = output;
                        Close();
                    }
                    else
                    {
                        BetInput.Text = _betTooBig;
                    }
                }
                else
                {
                    BetInput.Text = _betBelowZero;
                }
            }
            else
            {
                BetInput.Text = _betErrorMessage;
            }
        }

        private void BetInput_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = String.Empty;
        }

        private void BetInput_LostFocus(object sender, RoutedEventArgs e)
        {
            var box = (TextBox)sender;

            if (String.IsNullOrWhiteSpace(box.Text))
            {
                box.Text = _betPlaceholder;
            }
        }
    }
}
