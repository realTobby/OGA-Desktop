using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace OpenGameArtOrgClient.Models
{ 

    public class PostThumbnail
    {
        public PostThumbnail()
        {
            InitRelayCommands();
        }

        public string Name { get; set; } = string.Empty;
        public string PageURL { get; set; } = string.Empty;
        public string SourceURL { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;

        public ICommand OpenWebsiteCommand { get; set; } = null;
        public ICommand DownloadAssetCommand { get; set; } = null;

        private void InitRelayCommands()
        {
            DownloadAssetCommand = new Commands.RelayCommand(o => { DownloadAsset(o); }, o => true);
            OpenWebsiteCommand = new Commands.RelayCommand(o => { OpenAsset(o); }, o => true);
        }

        private void OpenAsset(object o)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = PageURL,
                UseShellExecute = true
            });
        }

        private void DownloadAsset(object o)
        {
            MessageBox.Show("Looks like I couldnt find a download for that asset...sorry!");
        }


    }
}
