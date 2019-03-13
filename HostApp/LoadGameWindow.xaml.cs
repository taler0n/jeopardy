using System;
using System.Windows;
using Game;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;

namespace HostApp
{
    /// <summary>
    /// Логика взаимодействия для LoadGameWindow.xaml
    /// </summary>
    public partial class LoadGameWindow : Window
    {
        private const string _loadedBadFile = "Невозможно открыть файл";
        private const string _unableToStartGame = "Невозможно загрузить игру";
        private const string _ready = "Готово";

        private GameObject _createdGame;

        public LoadGameWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.Filter = "XML-файлы|*.xml";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() == true)
            {
                var downloader = new XmlSerializer(typeof(GameObject));
                using (var reader = new StreamReader(dialog.FileName))
                {

                    try
                    {
                        GameObject newGame = (GameObject)downloader.Deserialize(reader);

                        if (newGame.IsReady())
                        {
                            _createdGame = newGame;
                            FileLabel.Visibility = Visibility.Visible;
                            FileNameLabel.Visibility = Visibility.Visible;
                            FileNameLabel.Content = dialog.SafeFileName;
                            ContinueButton.Visibility = Visibility.Visible;
                            StatusBar.Content = _ready;
                        }
                        else
                        {
                            StatusBar.Content = _unableToStartGame;
                        }
                    }
                    catch (Exception)
                    {
                        StatusBar.Content = _loadedBadFile;
                    }
                }
            }
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            var connectionWindow = new ClientConnectionWindow(_createdGame);
            Visibility = Visibility.Hidden;
            connectionWindow.ShowDialog();
            Close();
        }
    }
}
