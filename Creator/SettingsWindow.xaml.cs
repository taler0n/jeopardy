using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Game;

namespace Creator
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private Settings _settings;
        private Dictionary<TextBox, Setting> _boxToValue;
        public bool SettingsSaved;

        public SettingsWindow(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            SettingsSaved = false;
            _boxToValue = new Dictionary<TextBox, Setting>();

            _boxToValue.Add(Box_NumberOfPlayers, _settings.NumberOfPlayers);
            _boxToValue.Add(Box_RoundSize, _settings.RoundSize);
            _boxToValue.Add(Box_ThemeSize, _settings.ThemeSize);
            _boxToValue.Add(Box_FinalSize, _settings.FinalSize);
            _boxToValue.Add(Box_FirstQuestionValue, _settings.FirstQuestionValue);
            _boxToValue.Add(Box_QuestionStepValue, _settings.QuestionStepValue);
            _boxToValue.Add(Box_MaxThemeNameLength, _settings.MaxThemeNameLength);
            _boxToValue.Add(Box_MaxQuestionTextLength, _settings.MaxQuestionTextLength);

            foreach (var pair in _boxToValue)
            {
                pair.Key.Text = pair.Value.Value.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsSaved = true;
            Close();
        }

        private void Box_SettingValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            var setting = _boxToValue[box];
            int newValue;

            if (int.TryParse(box.Text, out newValue))
            {
                if (newValue >= setting.Minimum && newValue <= setting.Maximum)
                {
                    setting.Value = newValue;
                }
                else
                {
                    box.Text = setting.Value.ToString();
                }
            }
            else
            {
                box.Text = setting.Value.ToString();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
