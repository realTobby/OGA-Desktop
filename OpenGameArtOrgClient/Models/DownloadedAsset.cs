using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenGameArtOrgClient.Models
{
    public class DownloadedAsset
    { 
        public string FolderPath { get; set; }
        public string AssetName { get; set; }
        public string ThumbnailPath { get; set; }

        public ICommand OpenAssetFolderCommand { get; set; } = null;

        private void InitRelayCommands()
        {
            OpenAssetFolderCommand = new Commands.RelayCommand(o => { OpenAssetFolder(o); }, o => true);
        }

        private void OpenAssetFolder(object o)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = FolderPath,
                UseShellExecute = true
            });
        }

    }
}
