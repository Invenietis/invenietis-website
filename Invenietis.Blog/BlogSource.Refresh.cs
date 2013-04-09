using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using CK.Core;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Invenietis.Blog
{
    
    public partial class BlogSource
    {
        BlogRefreshResult _lastRefreshResult;
        BlogRefreshResult _lastSuccessfulRefreshResult;
        public bool SuccessfulUpdate
        {
            get { return _lastRefreshResult ==_lastSuccessfulRefreshResult; }
        }
        public BlogRefreshResult LastRefreshResult { get { return _lastRefreshResult; } }

        public BlogRefreshResult LastSuccessfulRefreshResult { get { return _lastSuccessfulRefreshResult; } }

        /// <summary>
        /// Uses .Net 4.5 <see cref="SyndicationFeed"/> to retrieve changes from the source.
        /// </summary>
        /// <returns>An object that summarizes the update from the <see cref="RSSUri"/>.</returns>
        public BlogRefreshResult RefreshFromUri(Uri uri)
        {
            _lastRefreshResult = LoadFromUri( uri );
            _lastRefreshResult.RefreshTime = DateTime.Now;
            return _lastRefreshResult;
        }

        internal BlogRefreshResult LoadFromUri(Uri uri)
        {
           using( XmlReader reader = XmlReader.Create( uri.ToString() ) )
           {
               SyndicationFeed feed = SyndicationFeed.Load(reader);
               if( Articles.Count == 0 )
               {
                   BlogArticle article = new BlogArticle(this);
                   foreach( SyndicationItem item in feed.Items )
                   {
                       AddArticle( article, item );
                   }
               }
               UpdateArticles(feed );

               RemoveArticles( feed);

               foreach(BlogArticle a in _articles)
               {
                   if( a.Status == BlogArticleStatus.HiddenByAuthor )
                   {
                       LastRefreshResult.DisappearedArticleCount += 1;
                   }
                   if( a.Status == BlogArticleStatus.New )
                   {
                       LastRefreshResult.NewArticleCount += 1;
                   }               
               }

               if( LastRefreshResult.IsSuccess )
               {
                   _lastSuccessfulRefreshResult = LastRefreshResult;
               }
            }
           return LastSuccessfulRefreshResult;           
        }

        private void CreateBlog( SyndicationFeed feed )
        {
            var source = Context.CreateBlogSource();
            source.Id = feed.Id;
            source.AuthorName = feed.Authors[0].Name;
            source.AuthorUri = feed.Authors[0].Uri;
            source.AuthorEMail = feed.Authors[0].Email;
            source.RSSUri = feed.BaseUri;
            if( feed.Language != null && StringComparer.OrdinalIgnoreCase.Compare( feed.Language, "en" ) == 0 )
            {
                source.BlogLanguage = BlogLanguage.English;
                source.BlogTitleEN = feed.Title.Text;
                source.BlogHtmlDescriptionEN = feed.Description.Text;
            }
            else
            {
                source.BlogLanguage = BlogLanguage.French;
                source.BlogTitleFR = feed.Title.Text;
                //source._blogHtmlDescriptionFR = feed.Description.Text;
            }

        }

        public void SaveContext( Uri uri, string path = null )
        {
            _lastRefreshResult = RefreshFromUri( uri );
            if( !_isDirty && _lastRefreshResult.IsSuccess )
            {
                _lastSuccessfulRefreshResult = _lastRefreshResult;
                Context.Save( path );
            }
        }

        public void UpdateArticles(SyndicationFeed Feed )
        {
            BlogArticle currentArticle = new BlogArticle(this);
           
            foreach( BlogArticle article in Articles )
            {
                foreach(SyndicationItem item in Feed.Items)
                {
                    if( item.Id == article.Id)
                    {
                        if( item.Title.Text != article.OriginalTitle )
                        {
                            article.OriginalTitle = item.Title.Text;
                            SendNotification();
                        }
                        if( item.LastUpdatedTime != article.LastModificationDate )
                        {
                            SendNotification();
                        }
                    }
                    else
                    {
                        currentArticle.Id = item.Id;
                        if( !Articles.Contains( currentArticle ) )
                        {
                            AddArticle( currentArticle, item );
                        }
                    }
                }

            }
        }

        private void RemoveArticles( SyndicationFeed Feed )
        {
            SyndicationItem currentItem = new SyndicationItem();
            foreach( BlogArticle article in Articles )
            {
                currentItem.Id = article.Id;
                if( !Feed.Items.Contains( currentItem ) )
                {
                    article.DestroyPublishedInfo();
                    article.Status = BlogArticleStatus.HiddenByAuthor;
                    _articles.Remove( article );
                }
            }
        }

        /// <summary>
        /// Send Notification to the administrator if the author updates at least one <see cref="BlogArticle"/>.
        /// </summary>
        internal void SendNotification( [CallerMemberName] string memberName = null )
        {
            Debug.WriteLine( "Administrator must verify this article" );
        }

        public void AddArticle(BlogArticle article, SyndicationItem item )
        {
            article.Status = BlogArticleStatus.New;
            article.OriginalTitle = item.Title.ToString();
            article.LastModificationDate = item.LastUpdatedTime;
            article.Uri = item.BaseUri;
            _articles.Add( article );
        }
    }
}