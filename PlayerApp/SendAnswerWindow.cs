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
using System.Net.Sockets;
using Game;
using TCPConnection;

namespace PlayerApp
{
    /// <summary>
    /// Логика взаимодействия для Send_AnswerWindow.xaml
    /// </summary>
    public partial class SendAnswerWindow : Window
    {
        private NetworkStream _stream;
        private Player _player;
        private const string _answerPlaceholder = "Введите ваш ответ...";

        public SendAnswerWindow(NetworkStream stream, Player player, string question)
        {
            InitializeComponent();
            _stream = stream;
            _player = player;
            NameLabel.Content = _player.Name;
            ScoreLabel.Content = _player.Score;
            AnswerInput.Text = _answerPlaceholder;
            QuestionBox.Text = question;
        }

        private void AnswerInput_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = String.Empty;
        }

        private void AnswerInput_LostFocus(object sender, RoutedEventArgs e)
        {
            var box = (TextBox)sender;

            if (String.IsNullOrWhiteSpace(box.Text))
            {
                box.Text = _answerPlaceholder;
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(AnswerInput.Text))
            {
                SendButton.Visibility = Visibility.Hidden;
                AnswerInput.IsReadOnly = true;
                AnswerInput.Foreground = Brushes.Yellow;
                string message = MessageSigns.AnswerMessage + AnswerInput.Text;
                byte[] data = Encoding.Unicode.GetBytes(message);
                _stream.Write(data, 0, data.Length);
            }
        }
    }
}
