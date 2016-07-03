using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ViewModel.ViewModel.Admin.Feed;

namespace MVCUI.RSS
{
    class FeedResult : ActionResult
    {
        #region Fields
        private readonly IList<SyndicationItem> _allItems;
        private readonly String _feedTitle;
        private readonly String _lang;
        #endregion

        #region Constracture
        public FeedResult(String feedTitle, IEnumerable<FeedItemViewModel> rssItems, String language="fa-IR")
        {
            _feedTitle = feedTitle;
            _allItems = MapTpSyndication(rssItems);
            _lang = language;
        }
        #endregion

        #region Methods

        private IList<SyndicationItem> MapTpSyndication(IEnumerable<FeedItemViewModel> rssItems)
        {
            IList<SyndicationItem> results = new List<SyndicationItem>();
            foreach (FeedItemViewModel item in rssItems)
            {
                Uri url = new Uri(item.Url);
                SyndicationItem feedItem = new SyndicationItem(item.Title.CorrectRtl(), SyndicationContent.CreateHtmlContent(item.Content.CorrectRtlBody()), url, item.Url.SHA1(), item.LastUpdatedTime)
                {
                    PublishDate = item.PublishDate
                };
                feedItem.Authors.Add(new SyndicationPerson(item.AuthorName, item.AuthorName, url.Host));
                results.Add(feedItem);
            }
            return results;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context.Equals(null))
                throw new ArgumentNullException("context is NULL");

            WriteToResponse(context.HttpContext);

        }

        private void WriteToResponse(HttpContextBase httpContext)
        {
            SyndicationFeed feed = new SyndicationFeed()
            {
                Title = new TextSyndicationContent(_feedTitle.CorrectRtl()),
                Language = _lang,
                Items = _allItems
            };
            AddChannellinks(httpContext, feed);

            String feedData = SysndicationFeedToString(feed);
            feedData = feedData.Replace("xmlns:a10", "xmlns:atom").Replace("a10:", "atom:");

            HttpResponseBase response = httpContext.Response;
            response.ContentEncoding = Encoding.UTF8;
            response.ContentType= "application/rss+xml";
            response.Write(response);
            response.End();
        }

        private String SysndicationFeedToString(SyndicationFeed feed)
        {
            using(MemoryStream memoryStream=new MemoryStream())
            {
                using(XmlWriter writeXML=XmlWriter.Create(memoryStream, new XmlWriterSettings() { Indent=true }))
                {
                    Rss20FeedFormatter formatter3 = new Rss20FeedFormatter(feed);
                    formatter3.WriteTo(writeXML);
                    writeXML.Close();
                }
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        private void AddChannellinks(HttpContextBase httpContext, SyndicationFeed feed)
        {
            if (httpContext.Request.Url.Equals(null))
                return;
            Uri baseUrl = new UriBuilder(httpContext.Request.Url.Scheme, httpContext.Request.Url.Host).Uri;
            Uri feedLink = new Uri(baseUrl, httpContext.Request.RawUrl);
            feed.Links.Add(SyndicationLink.CreateSelfLink(feedLink));
            feed.Links.Add(new SyndicationLink { Uri = baseUrl, RelationshipType = "alternate" });
        }

        #endregion
    }
}
