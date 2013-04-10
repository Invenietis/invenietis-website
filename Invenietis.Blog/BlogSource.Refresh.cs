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
                  
                   foreach( SyndicationItem item in feed.Items )
                   {
                       BlogArticle article = new BlogArticle( this );
                       article.Id = item.Id;
                       article.Status = BlogArticleStatus.New;
                       article.OriginalTitle = item.Title.Text;
                       article.LastModificationDate = item.LastUpdatedTime;
                       article.Uri = item.BaseUri;
                       _articles.Add( article );
                   }
               }
               UpdateArticles(feed );

               RemoveArticles(feed);

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
            foreach( BlogArticle article in Articles )
            {
                foreach( SyndicationItem item in Feed.Items )
                {
                    if( item.Id == article.Id )
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
                }
            }
            IEnumerable<SyndicationItem> NewItems = Enumerable.Empty<SyndicationItem>();
            for( int i=0; i < Articles.Count; i++ )
            {
                NewItems = from item in Feed.Items
                           where !Articles[i].Id.Any()
                           select item;
            }
            
            if(NewItems.Count()!= 0 )
            {
                foreach( SyndicationItem item in NewItems )
                {
                    BlogArticle currentArticle = new BlogArticle( this );
                    currentArticle.Id = Feed.Items.GetEnumerator().Current.Id;
                    currentArticle.Status = BlogArticleStatus.New;
                    currentArticle.OriginalTitle = Feed.Items.GetEnumerator().Current.Title.Text;
                    currentArticle.LastModificationDate = Feed.Items.GetEnumerator().Current.LastUpdatedTime;
                    currentArticle.Uri = Feed.Items.GetEnumerator().Current.BaseUri;
                    _articles.Add( currentArticle );
                }
            }
            else
            {
                throw new ArgumentNullException("No new items");
            }
                 
        }

        private void RemoveArticles( SyndicationFeed Feed )
        {
            IEnumerable<BlogArticle> removedArticles = Enumerable.Empty<BlogArticle>();
            foreach( SyndicationItem item in Feed.Items )
            {
                removedArticles = from article in Articles
                                  where !item.Id.Any()
                                  select article;
            }
            if( removedArticles.Count() != 0 )
            {
                foreach( BlogArticle a in removedArticles )
                {
                    a.DestroyPublishedInfo();
                    a.Status = BlogArticleStatus.HiddenByAuthor;
                    _articles.Remove( a );
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
    }
}