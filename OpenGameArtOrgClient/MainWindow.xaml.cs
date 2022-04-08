using OpenGameArtOrgClient.ViewModels;
using OpenGameArtOrgData;
using System.Windows;

namespace OpenGameArtOrgClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        private DataFinder _dataFinder;

        private const string SETTINGS_FILE_NAME = "CRAZYCOMPLEXSETTINGSFILE.SETTINGS";

        public MainWindow()
        {
            _dataFinder = new DataFinder();
            _viewModel = new MainWindowViewModel();

            LoadSettingsFile();

            
            this.DataContext = _viewModel;

            InitializeComponent();

            ShowPopularOfWeek();

        }

        private void LoadSettingsFile()
        {
            string settingsFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, SETTINGS_FILE_NAME);

            if(System.IO.File.Exists(settingsFile))
            {
                string[] settings = System.IO.File.ReadAllLines(settingsFile);

                _viewModel.DownloadDirectory = settings[0].Split('=')[1];

            }
            else
            {
                MessageBox.Show("Looks like its your first time using this application, please set a directory where you want the assets to be stored!");
                SetDownloadDirectory();
                MessageBox.Show("Your download directory now is: " + _viewModel.DownloadDirectory + " You can change that at all times in the settings tab!");
            }
        }

        private void SetDownloadDirectory()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            _viewModel.DownloadDirectory = dialog.SelectedPath;
            SaveSettingsFile();
        }

        private void SaveSettingsFile()
        {
            string settingsFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, SETTINGS_FILE_NAME);
            string[] settings = new string[1];

            settings[0] = "downloadDirectory=" + _viewModel.DownloadDirectory;

            System.IO.File.WriteAllLines(settingsFile, settings);
        }

        private void ShowPopularOfWeek()
        {
            _viewModel.PopularWeek = new System.Collections.ObjectModel.ObservableCollection<OpenGameArtOrgData.Models.PostThumbnail>(_dataFinder.GetPopularWeek());
        }

        private void SearchAssets()
        {
            _viewModel.SearchAssets = new System.Collections.ObjectModel.ObservableCollection<OpenGameArtOrgData.Models.PostThumbnail>(_dataFinder.Search(_viewModel.SearchString, _viewModel.CurrentPageCount));
        }

        private void CleanAssetSearch()
        {
            _viewModel.SearchAssets.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchAssets();
        }

        private void btn_pageBack_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentPageCount -= 1;
            CleanAssetSearch();
            SearchAssets();
        }

        private void btn_pageNext_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentPageCount++;
            CleanAssetSearch();
            SearchAssets();
        }

        private void btn_setDownloadDirectory_Click(object sender, RoutedEventArgs e)
        {
            SetDownloadDirectory();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettingsFile();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Asset: " + _viewModel.SelectedPopularWeekItem.Name);
        }
    }
}
