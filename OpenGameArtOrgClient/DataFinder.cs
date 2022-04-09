using HtmlAgilityPack;
using OpenGameArtOrgClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenGameArtOrgClient
{
    public class DataFinder
    {
        public string DOWNLOAD_DIRECTORY = string.Empty;

        public DataFinder(string dlDirectory)
        {
            DOWNLOAD_DIRECTORY = dlDirectory;
        }

        // Class DataFinder, supposed to only get data from opengameart.org
        private HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        public List<PostThumbnail> GetPopularWeek()
        {
            List<PostThumbnail> data = new List<PostThumbnail>();

            HtmlDocument page = GetDocument("https://opengameart.org/");

            var viewContent = page.DocumentNode.SelectSingleNode("//*[@id=\"block-views-art-block-9\"]/div/div/div[1]");

            foreach(var thumbnailRoot in viewContent.ChildNodes)
            {
                if (thumbnailRoot.Name == "div")
                {
                    PostThumbnail thumbnail = new PostThumbnail();

                    HtmlNode NameAndPageNode = thumbnailRoot.ChildNodes[1].ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0];
                    HtmlNode previewImageNode = thumbnailRoot.ChildNodes[1].ChildNodes[2].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0];

                    thumbnail.Name = NameAndPageNode.InnerText;
                    thumbnail.SourceURL = "https://opengameart.org/";
                    thumbnail.PageURL = thumbnail.SourceURL + NameAndPageNode.GetAttributeValue("href", "notfound");
                    thumbnail.ImageURL = previewImageNode.GetAttributeValue("src", "notfound");

                    thumbnail.DownloadAssetCommand = new Commands.RelayCommand(o => { DownloadAssets(thumbnail); }, o => true);

                    data.Add(thumbnail);
                }
            }


            return data;
        }

        public List<PostThumbnail> Search(string searchString, int pageCount)
        {
            List<PostThumbnail> data = new List<PostThumbnail>();

            HtmlDocument page = GetDocument("https://opengameart.org/art-search-advanced?keys=" + searchString + "&page=" + pageCount + "&title=&field_art_tags_tid_op=or&field_art_tags_tid=&name=&field_art_type_tid%5B%5D=9&sort_by=count&sort_order=DESC&items_per_page=24&Collection=");

            var viewContent = page.DocumentNode.SelectSingleNode("//*[@id=\"block-system-main\"]/div/div/div[2]");

            foreach (var thumbnailRoot in viewContent.ChildNodes)
            {
                if (thumbnailRoot.Name == "div")
                {
                    PostThumbnail thumbnail = new PostThumbnail();

                    HtmlNode NameAndPageNode = thumbnailRoot.ChildNodes[1].ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0];
                    HtmlNode previewImageNode = thumbnailRoot.ChildNodes[1].ChildNodes[2].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0];

                    thumbnail.Name = NameAndPageNode.InnerText;
                    thumbnail.SourceURL = "https://opengameart.org/";
                    thumbnail.PageURL = thumbnail.SourceURL + NameAndPageNode.GetAttributeValue("href", "notfound");
                    thumbnail.ImageURL = previewImageNode.GetAttributeValue("src", "https://opengameart.org/sites/default/files/styles/thumbnail/public/audio_preview/tomaszkucza-felicitousforest-loop2_0.mp3.png");

                    thumbnail.DownloadAssetCommand = new Commands.RelayCommand(o => { DownloadAssets(thumbnail); }, o => true);

                    data.Add(thumbnail);
                }
            }


            return data;

        }

        private void DownloadAssets(object o)
        {
            if(o != null)
            {
                PostThumbnail assetPost = (PostThumbnail)o;

                List<AssetFile> assetDownloads = new List<AssetFile>();

                string assetName = assetPost.Name;

                HtmlDocument assetPage = GetDocument(assetPost.PageURL);

                HtmlNode fileRoot = assetPage.DocumentNode.SelectSingleNode("//div[@class='" + "field field-name-field-art-files field-type-file field-label-above" + "']").ChildNodes[1];
                foreach (var fileNode in fileRoot.ChildNodes)
                {
                    AssetFile asset = new AssetFile();

                    string fileName = fileNode.ChildNodes[0].ChildNodes[2].InnerHtml;
                    string directDownload = fileNode.ChildNodes[0].ChildNodes[2].GetAttributeValue("href", "noDownload");

                    asset.Name = fileName;
                    asset.DirectURL = directDownload;

                    assetDownloads.Add(asset);
                }

                using (WebClient client = new WebClient())
                {
                    foreach (AssetFile asset in assetDownloads)
                    {
                        // DOWNLOAD!!!!
                        string path = System.IO.Path.Combine(DOWNLOAD_DIRECTORY, assetName, asset.Name);
                        
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(DOWNLOAD_DIRECTORY, assetName));

                        client.DownloadFile(asset.DirectURL, path);

                        if(assetPost.ImageURL != "notfound")
                        {
                            string thumbnailPath = System.IO.Path.Combine(DOWNLOAD_DIRECTORY, assetName, "thumbnail.png");
                            client.DownloadFile(assetPost.ImageURL, thumbnailPath);
                        }
                        

                    }

                }

            }
            



        }


        

    }
}
