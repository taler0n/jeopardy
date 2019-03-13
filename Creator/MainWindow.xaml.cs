using System.Windows;
using Game;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;

namespace Creator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            var settings = new Settings();
            var settingsWindow = new SettingsWindow(settings);
            settingsWindow.ShowDialog();
            Visibility = Visibility.Visible;

            if (settingsWindow.SettingsSaved)
            {
                var newGameWindow = new GameCreator(settings);
                newGameWindow.Owner = this;
                Visibility = Visibility.Hidden;
                newGameWindow.Show();
            }
        }

        private void EditExistingGameButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML-файлы|*.xml";
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() == true)
            {
                var downloader = new XmlSerializer(typeof(GameObject));
                using (var reader = new StreamReader(dialog.FileName))
                {

                    try
                    {
                        GameObject newGame = (GameObject)downloader.Deserialize(reader);
                        var newGameWindow = new GameCreator(newGame);
                        newGameWindow.Owner = this;
                        Visibility = Visibility.Hidden;
                        newGameWindow.Show();
                    }
                    catch
                    {
                        
                    }
                }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
