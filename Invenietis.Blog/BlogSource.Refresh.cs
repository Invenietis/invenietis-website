﻿using System;
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

        bool _successfulUpdate;

        public bool SuccessfulUpdate
        {
            get { return _successfulUpdate; }
            set
            {
                if( value != _successfulUpdate )
                {
                    _successfulUpdate = value;
                }
            }
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
            BlogRefreshResult currentRefreshResult = new BlogRefreshResult();
           using( XmlReader reader = XmlReader.Create( uri.ToString() ) )
           {
               SyndicationFeed feed = SyndicationFeed.Load(reader);
               BlogSource source = LoadBlogData(Context, feed );
               List<BlogArticle> articles = LoadArticles( source,feed );

               foreach(BlogArticle a in articles)
               {
                   if( a.Status == BlogArticleStatus.HiddenByAuthor )
                   {
                       currentRefreshResult.DisappearedArticleCount += 1;
                   }
                   if( a.Status == BlogArticleStatus.New )
                   {
                       currentRefreshResult.NewArticleCount += 1;
                   }
                   
               }
               currentRefreshResult.RefreshTime = DateTime.Now;
               if( !currentRefreshResult.IsSuccess )
               {
                   currentRefreshResult = LastRefreshResult;
               }
            }
           return currentRefreshResult;           
        }

        private BlogSource LoadBlogData( BlogContext ctx, SyndicationFeed feed )
        {
            BlogSource source = new BlogSource(ctx);
            source._authorName = feed.Authors[0].Name;
            source._authorUri = feed.Authors[0].Uri;
            source._authorEMail = feed.Authors[0].Email;
            source._rssUri = feed.BaseUri;
            if( feed.Language != null && StringComparer.OrdinalIgnoreCase.Compare( feed.Language, "en" ) == 0 )
            {
                source._blogLanguage = BlogLanguage.English;
                source._blogTitleEN = feed.Title.Text;
                source._blogHtmlDescriptionEN = feed.Description.Text;
            }
            else
            {
                source._blogLanguage = BlogLanguage.French;
                source._blogTitleFR = feed.Title.Text;
                //source._blogHtmlDescriptionFR = feed.Description.Text;
            }
            return source;
        }

        internal List<BlogArticle> LoadArticles( BlogSource @this, SyndicationFeed Feed )
        {
            using( IEnumerator<SyndicationItem> item = Feed.Items.GetEnumerator() )
            {
                BlogArticle currentArticle = new BlogArticle();

                if( item.MoveNext() )
                {
                    currentArticle._lastModificationDate = item.Current.LastUpdatedTime;
                    currentArticle._originalTitle = item.Current.Title.Text;
                    currentArticle.Id = item.Current.Id;
                    @this._articles.Add( currentArticle );
                }

            }
            return @this._articles;
        }

        public void Update( Uri uri, string path = null )
        {
            _lastRefreshResult = RefreshFromUri( uri );
            if( !_isDirty && _lastRefreshResult.IsSuccess )
            {
                _lastSuccessfulRefreshResult = _lastRefreshResult;
                _successfulUpdate = true;
                Context.Save( path );
            }
            else
            {
                _successfulUpdate = false;
            }
        }

        public void UpdateArticles( BlogSource @this, SyndicationFeed Feed )
        {
            SyndicationItem currentItem = new SyndicationItem();
            foreach( BlogArticle article in @this.Articles )
            {
                using( IEnumerator<SyndicationItem> item = Feed.Items.GetEnumerator() )
                {
                    if( item.MoveNext() )
                    {
                        if( article.Id == item.Current.Id )
                        {
                            currentItem = item.Current;
                            if( currentItem.Title.Text != article.OriginalTitle )
                            {
                                article.OriginalTitle = currentItem.Title.Text;
                                SendNotification();
                            }
                            if( currentItem.LastUpdatedTime != article.LastModificationDate )
                            {
                                SendNotification();
                            }

                        }
                        else
                            throw new InvalidOperationException( "Id does not exist" );
                    }
                }
            }
        }

        /// <summary>
        /// Send Notification to the administrator if the author updates at least one <see cref="BlogArticle"/>.
        /// </summary>
        internal void SendNotification( [CallerMemberName] string memberName = null )
        {
            throw new NotImplementedException();
        }
    }
}