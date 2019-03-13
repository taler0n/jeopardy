using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Game;

namespace Creator
{
    /// <summary>
    /// Логика взаимодействия для FinalCreator.xaml
    /// </summary>
    public partial class FinalCreator : Window
    {
        private const string _styleName = "YellowContentStyle";

        private FinalRound _final;
        private Settings _settings;
        private int _activeQuestionIndex;
        private List<Button> _questionButtons;

        public FinalCreator(FinalRound final, Settings settings)
        {
            InitializeComponent();
            _final = final;
            _settings = settings;
            _questionButtons = new List<Button>();
            DrawQuestionGrid();
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

        private void UpdateButtons()
        {
            for (int i = 0; i < _questionButtons.Count; i++)
            {
                _questionButtons[i].Content = _final[i].Name;
            }
        }

        private void UpdateActiveButton()
        {
            for (int i = 0; i < _questionButtons.Count; i++)
            {
                if (i == _activeQuestionIndex)
                {
                    _questionButtons[i].Foreground = Brushes.Red;
                }
                else
                {
                    _questionButtons[i].Foreground = Brushes.Yellow;
                }
            }
        }

        private void QuestionButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            _activeQuestionIndex = _questionButtons.IndexOf(button);
            UpdateActiveButton();

            SaveButton.Visibility = Visibility.Visible;
            AnswerLabel.Visibility = Visibility.Visible;
            AnswerTextBox.Visibility = Visibility.Visible;
            AnswerTextBox.Text = _final[_activeQuestionIndex].AnswerText;
            QuestionLabel.Visibility = Visibility.Visible;
            QuestionTextBox.Visibility = Visibility.Visible;
            QuestionTextBox.Text = _final[_activeQuestionIndex].QuestionText;
            NameLabel.Visibility = Visibility.Visible;
            NameTextBox.Visibility = Visibility.Visible;
            NameTextBox.Text = _final[_activeQuestionIndex].Name;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _final[_activeQuestionIndex].QuestionText = QuestionTextBox.Text;
            _final[_activeQuestionIndex].AnswerText = AnswerTextBox.Text;
            _final[_activeQuestionIndex].Name = NameTextBox.Text;
            UpdateButtons();
            SaveButton.Visibility = Visibility.Hidden;
            AnswerLabel.Visibility = Visibility.Hidden;
            AnswerTextBox.Visibility = Visibility.Hidden;
            QuestionLabel.Visibility = Visibility.Hidden;
            QuestionTextBox.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            NameTextBox.Visibility = Visibility.Hidden;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AnswerTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(AnswerTextBox.Text) || AnswerTextBox.Text.Length > _settings.MaxQuestionTextLength.Value)
            {
                AnswerTextBox.Text = _final[_activeQuestionIndex].AnswerText;
            }
        }

        private void QuestionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(QuestionTextBox.Text) || QuestionTextBox.Text.Length > _settings.MaxQuestionTextLength.Value)
            {
                QuestionTextBox.Text = _final[_activeQuestionIndex].QuestionText;
            }
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NameTextBox.Text) || NameTextBox.Text.Length > _settings.MaxThemeNameLength.Value)
            {
                NameTextBox.Text = _final[_activeQuestionIndex].Name;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Owner != null)
            {
                Owner.Visibility = Visibility.Visible;
            }
        }
    }
}
