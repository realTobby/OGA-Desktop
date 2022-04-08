using OpenGameArtOrgData.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGameArtOrgClient.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnProperyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<PostThumbnail> _popularWeek;
        private PostThumbnail _selectedPopularWeekItem;


        private string _searchString = string.Empty;

        private ObservableCollection<PostThumbnail> _searchAssets;
        private PostThumbnail _selectedSearchAssetItem;

        private int _currentPageCount = 0;

        private string _downloadDirectory = string.Empty;


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

    }
}
