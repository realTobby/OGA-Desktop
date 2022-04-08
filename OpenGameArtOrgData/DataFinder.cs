using HtmlAgilityPack;
using OpenGameArtOrgData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGameArtOrgData
{
    public class DataFinder
    {
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
                    thumbnail.ImageURL = previewImageNode.GetAttributeValue("src", "https://opengameart.org/sites/default/files/styles/thumbnail/public/audio_preview/tomaszkucza-felicitousforest-loop2_0.mp3.png");
                    
                    data.Add(thumbnail);
                }
            }


            return data;
        }


        public List<PostThumbnail> Search(string searchString, int pageCount)
        {
            // 
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

                    data.Add(thumbnail);
                }
            }


            return data;

        }

    }
}
