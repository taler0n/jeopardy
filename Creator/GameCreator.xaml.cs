using System.Windows;
using Game;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;

namespace Creator
{
    public partial class GameCreator : Window
    {
        private const string _roundName = "Раунд";
        private GameObject _game;

        public GameCreator(Settings settings)
        {
            InitializeComponent();
            _game = new GameObject(settings);
        }

        public GameCreator(GameObject game)
        {
            InitializeComponent();
            _game = game;

            for (int i = 0; i < _game.Rounds.Count; i++)
            {
                RoundBox.Items.Add(_roundName + (i + 1).ToString());
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var round = new Round(_game.Settings);
            _game.Rounds.Add(round);
            RoundBox.Items.Add(_roundName + _game.Rounds.Count);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoundBox.SelectedItem != null)
            {
                int index = int.Parse(((string)RoundBox.SelectedItem).Substring(_roundName.Length)) - 1;
                var round = _game.Rounds[index];
                var roundWindow = new RoundCreator(round, _game.Settings);
                roundWindow.Owner = this;
                Visibility = Visibility.Hidden;
                roundWindow.Show();
            }
        }

        private void EditFinalButton_Click(object sender, RoutedEventArgs e)
        {
            var finalRoundWindow = new FinalCreator(_game.Final, _game.Settings);
            finalRoundWindow.Owner = this;
            Visibility = Visibility.Hidden;
            finalRoundWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoundBox.SelectedItem != null)
            {
                int index = int.Parse(((string)RoundBox.SelectedItem).Substring(_roundName.Length)) - 1;
                _game.Rounds.RemoveAt(index);
                RoundBox.Items.Clear();

                for (int i = 0; i < _game.Rounds.Count; i++)
                {
                    string num = (i + 1).ToString();
                    RoundBox.Items.Add(_roundName + num);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML-файлы|*.xml";
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameObject));
                using (var writer = new StreamWriter(dialog.FileName))
                {
                    serializer.Serialize(writer, _game);
                }
                Close();
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
