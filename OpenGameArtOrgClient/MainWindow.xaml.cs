using OpenGameArtOrgClient.Models;
using OpenGameArtOrgClient.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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
            
            _viewModel = new MainWindowViewModel();
            this.DataContext = _viewModel;

            LoadSettingsFile();

            _dataFinder = new DataFinder(_viewModel.DownloadDirectory);


            _viewModel.PropertyChanged += CheckDownloadDirectoyChanged;



            InitializeComponent();

            ShowPopularOfWeek();

        }

        private void LoadDownloadedAssets()
        {
            _viewModel.DownloadedAssets.Clear();

            if(System.IO.Directory.Exists(_viewModel.DownloadDirectory))
            {
                string[] assetFolder = System.IO.Directory.GetDirectories(_viewModel.DownloadDirectory);
                foreach(string assetRootPath in assetFolder)
                {
                    string assetName = System.IO.Path.GetFileName(assetRootPath);

                    string[] assets = System.IO.Directory.GetFiles(assetRootPath);

                    assetName = assetName + " (Files: " + (assets.Length-1) + ")";

                    string thumbnailPath = System.IO.Path.Combine(assetRootPath, "thumbnail.png");

                    DownloadedAsset dAsset = new DownloadedAsset();
                    dAsset.FolderPath = assetRootPath;
                    dAsset.AssetName = assetName;

                    if (System.IO.File.Exists(thumbnailPath))
                        dAsset.ThumbnailPath = thumbnailPath;

                    _viewModel.DownloadedAssets.Add(dAsset);

                }
            }
        }

        private void CheckDownloadDirectoyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_viewModel.DownloadDirectory))
            {
                _dataFinder.DOWNLOAD_DIRECTORY = _viewModel.DownloadDirectory;
            }
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
            _viewModel.PopularWeek = new System.Collections.ObjectModel.ObservableCollection<PostThumbnail>(_dataFinder.GetPopularWeek());
        }

        private void SearchAssets()
        {
            _viewModel.SearchAssets = new System.Collections.ObjectModel.ObservableCollection<PostThumbnail>(_dataFinder.Search(_viewModel.SearchString, _viewModel.CurrentPageCount));
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

        private void btn_openDownloadDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.Directory.Exists(_viewModel.DownloadDirectory))
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = _viewModel.DownloadDirectory,
                    UseShellExecute = true
                });
            }
            else
                MessageBox.Show("Looks like I cannot find your download directory...You sure you correctly entered it?");

            
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(e.OriginalSource is TabControl)
            {
                TabControl tabControl = (TabControl)e.OriginalSource as TabControl;
                if (tabControl.SelectedIndex == 0)
                    LoadDownloadedAssets();
            }
        }
    }
}
