using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Game;

namespace Creator
{
    /// <summary>
    /// Логика взаимодействия для RoundCreator.xaml
    /// </summary>
    public partial class RoundCreator : Window
    {
        private class Coords
        {
            public int Row;
            public int Column;

            public Coords(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }

        private const string _styleName = "YellowContentStyle";
        private const int _questionBorderThickness = 5;

        private Round _round;
        private Settings _settings;
        private int _mult;
        private int _maxMult;
        private Dictionary<TextBox, int> _nameBoxes;
        private Dictionary<Button, Coords> _questionButtons;

        public RoundCreator(Round round, Settings settings)
        {
            InitializeComponent();
            _round = round;
            _settings = settings;
            _mult = _round[0].GetQuestionValue(0) / _settings.FirstQuestionValue.Value;
            _maxMult = (2000000000 / (_settings.FirstQuestionValue.Value + (_settings.QuestionStepValue.Value * (_settings.ThemeSize.Value - 1))));
            _nameBoxes = new Dictionary<TextBox, int>();
            _questionButtons = new Dictionary<Button, Coords>();
            MultiplierBox.Text = _mult.ToString();
            DrawQuestionGrid();
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
                TextBox themeName = new TextBox();
                _nameBoxes.Add(themeName, i);
                themeName.Style = (Style)FindResource(_styleName);
                themeName.Foreground = Brushes.White;
                themeName.Text = _round[i].Name;
                Grid.SetColumn(themeName, 0);
                Grid.SetRow(themeName, i);
                themeName.AddHandler(TextBox.LostFocusEvent, new RoutedEventHandler(ThemeName_LostFocus));
                QuestionGrid.Children.Add(themeName);
            }

            for (int i = 0; i < _settings.RoundSize.Value; i++)
            {
                for (int j = 0; j < _settings.ThemeSize.Value; j++)
                {
                    Button question = new Button();
                    _questionButtons.Add(question, new Coords(i, j));
                    question.Style = (Style)FindResource(_styleName);
                    question.Content = _round[i].GetQuestionValue(j);
                    Grid.SetColumn(question, j + 1);
                    Grid.SetRow(question, i);
                    question.AddHandler(Button.ClickEvent, new RoutedEventHandler(QuestionButton_Click));
                    QuestionGrid.Children.Add(question);
                }
            }

            UpdateButtons();
        }
        
        private void UpdateButtons()
        {
            foreach (var pair in _questionButtons)
            {
                int row = pair.Value.Row;
                int column = pair.Value.Column;
                pair.Key.Content = _round[row].GetQuestionValue(column);

                if (_round[row][column].IsReady())
                {
                    pair.Key.BorderThickness = new Thickness(_questionBorderThickness);
                }
                else
                {
                    pair.Key.BorderThickness = new Thickness(0);
                }
            }
        }

        private void ThemeName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox themeName = (TextBox)sender;
            int index = _nameBoxes[themeName];

            if (!String.IsNullOrWhiteSpace(themeName.Text) && themeName.Text.Length < _settings.MaxThemeNameLength.Value)
            {
                _round[index].Name = themeName.Text;
            }
            else
            {
                themeName.Text = _round[index].Name;
            }
        }

        private void QuestionButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var coords = _questionButtons[button];
            var question = _round[coords.Row][coords.Column];
            var questionWindow = new QuestionCreator(question, _settings);
            Visibility = Visibility.Hidden;
            questionWindow.ShowDialog();
            Visibility = Visibility.Visible;
            UpdateButtons();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MultiplierBox_LostFocus(object sender, RoutedEventArgs e)
        {
            int newMult;

            if (int.TryParse(MultiplierBox.Text, out newMult))
            {
                if (newMult > 0 && newMult <= _maxMult)
                {
                    _mult = newMult;

                    for (int i = 0; i < _settings.RoundSize.Value; i++)
                    {
                        _round[i].SetMultiplier(_mult, _settings);
                    }

                    UpdateButtons();
                    return;
                }
            }

            MultiplierBox.Text = _mult.ToString();
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
