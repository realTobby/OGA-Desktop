using OpenGameArtOrgClient.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenGameArtOrgClient.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnProperyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<PostThumbnail> _popularWeek = new ObservableCollection<PostThumbnail>();
        private PostThumbnail _selectedPopularWeekItem = new PostThumbnail();


        private string _searchString = string.Empty;

        private ObservableCollection<PostThumbnail> _searchAssets = new ObservableCollection<PostThumbnail>();
        private PostThumbnail _selectedSearchAssetItem = new PostThumbnail();


        private ObservableCollection<DownloadedAsset> _downloadedAssets = new ObservableCollection<DownloadedAsset>();
        private DownloadedAsset _selectedDownloadedAsset = new DownloadedAsset();

        private ObservableCollection<PostThumbnail> _challengeAssets = new ObservableCollection<PostThumbnail>();
        private PostThumbnail _selectedChallengeAssetItem = new PostThumbnail();


        private int _currentPageCount = 0;

        private string _downloadDirectory = string.Empty;


        public PostThumbnail SelectedChallengeAssetItem
        {
            get
            {
                return _selectedChallengeAssetItem;
            }
            set
            {
                if(_selectedChallengeAssetItem != value)
                {
                    _selectedChallengeAssetItem = value;
                    OnProperyChanged(nameof(SelectedChallengeAssetItem));
                }
            }
        }

        public ObservableCollection<PostThumbnail> ChallengeAssets
        {
            get
            {
                return _challengeAssets;
            }
            set
            {
                if (_challengeAssets != value)
                {
                    _challengeAssets = value;
                    OnProperyChanged(nameof(ChallengeAssets));
                }
            }
        }
        

        public ObservableCollection<PostThumbnail> PopularWeek
        {
            get
            {
                return _popularWeek;
            }
            set
            {
                if(_popularWeek != value)
                {
                    _popularWeek = value;
                    OnProperyChanged(nameof(PopularWeek));
                }
            }
        }

        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                if(_searchString != value)
                {
                    _searchString = value;
                    OnProperyChanged(nameof(SearchString));
                }
            }
        }

        public ObservableCollection<PostThumbnail> SearchAssets
        {
            get
            {
                return _searchAssets;
            }
            set
            {
                if(_searchAssets != value)
                {
                    _searchAssets = value;
                    OnProperyChanged(nameof(SearchAssets));
                }
            }
        }

        public int CurrentPageCount
        {
            get
            {
                return _currentPageCount;
            }
            set
            {
                if(_currentPageCount != value)
                {
                    _currentPageCount = value;
                    OnProperyChanged(nameof(CurrentPageCount));
                }
            }
        }

        public string DownloadDirectory
        {
            get
            {
                return _downloadDirectory;
            }
            set
            {
                if(_downloadDirectory != value)
                {
                    _downloadDirectory = value;
                    OnProperyChanged(nameof(DownloadDirectory));
                }
            }
        }
        
        public PostThumbnail SelectedPopularWeekItem
        {
            get
            {
                return _selectedPopularWeekItem;
            }
            set
            {
                if(_selectedPopularWeekItem != value)
                {
                    _selectedPopularWeekItem = value;
                    OnProperyChanged(nameof(SelectedPopularWeekItem));
                }
            }
        }

        public PostThumbnail SelectedSearchAssetItem
        {
            get
            {
                return _selectedSearchAssetItem;
            }
            set
            {
                if(_selectedSearchAssetItem != value)
                {
                    _selectedSearchAssetItem = value;
                    OnProperyChanged(nameof(SelectedSearchAssetItem));
                }
            }
        }

        public ObservableCollection<DownloadedAsset> DownloadedAssets
        {
            get
            {
                return _downloadedAssets;
            }
            set
            {
                if(_downloadedAssets != value)
                {
                    _downloadedAssets = value;
                    OnProperyChanged(nameof(DownloadedAssets));
                }
            }
        }

        public DownloadedAsset SelectedDownloadedAsset
        {
            get
            {
                return _selectedDownloadedAsset;
            }
            set
            {
                if(_selectedDownloadedAsset != value)
                {
                    _selectedDownloadedAsset = value;
                    OnProperyChanged(nameof(SelectedDownloadedAsset));
                }
            }
        }

    }
}
