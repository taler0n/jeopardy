using System;
using System.Windows;
using Game;

namespace Creator
{
    
    public partial class QuestionCreator : Window
    {
        private Question _question;
        private Settings _settings;

        public QuestionCreator(Question question, Settings settings)
        {
            InitializeComponent();
            _question = question;
            _settings = settings;
            QuestionTextBox.Text = _question.QuestionText;
            AnswerTextBox.Text = _question.AnswerText;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _question.QuestionText = QuestionTextBox.Text;
            _question.AnswerText = AnswerTextBox.Text;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AnswerTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(AnswerTextBox.Text) || AnswerTextBox.Text.Length > _settings.MaxQuestionTextLength.Value)
            {
                AnswerTextBox.Text = _question.AnswerText;
            }
        }

        private void QuestionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(QuestionTextBox.Text) || QuestionTextBox.Text.Length > _settings.MaxQuestionTextLength.Value)
            {
                QuestionTextBox.Text = _question.QuestionText;
            }
        }
    }
}
