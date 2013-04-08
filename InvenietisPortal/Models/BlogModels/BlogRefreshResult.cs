using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Invenietis.Blog
{
    [Serializable]
    /// <summary>
    /// Captures the work of <see cref="BlogSource.RefreshFromUri"/>.
    /// </summary>
    public class BlogRefreshResult
    {
        private DateTime _refreshTime;
        private string _errorMessage;
        private int _newArticleCount;
        private int _disappearedArticleCount;
        BlogContext _ctx = new BlogContext( null );

        public DateTime RefreshTime 
        { 
            get { return _refreshTime; }
            set
            {
                if( value != _refreshTime )
                {
                    _refreshTime = value;
                }
            }
        }

        public bool IsSuccess { get { return _errorMessage == null; } }

        public string ErrorMessage { 
            get { return _errorMessage; }
            set
            {
                if( value != _errorMessage )
                {
                    _errorMessage = value;
                }
            }
        }

        public int NewArticleCount
        { 
            get { return _newArticleCount; }
            set
            {
                if( value != _newArticleCount )
                {
                    _newArticleCount = value;
                }
            }
        }

        public int DisappearedArticleCount 
        { 
            get { return _disappearedArticleCount; }
            set
            {
                if( value != _disappearedArticleCount )
                {
                    _disappearedArticleCount = value;
                }
            }
        }

        void UpdateArticles( List<BlogArticle> articles, List<SyndicationItem> items )
        {
            BlogSource Source = _ctx.CreateBlogSource();
            BlogArticle currentArticle = new BlogArticle();
            SyndicationItem currentItem = new SyndicationItem();
            using( IEnumerator<SyndicationItem> item = items.GetEnumerator() )
            {
                
                if( item.MoveNext() )
                {
                    currentArticle.Id = item.Current.Id;
                    if( !articles.Contains( currentArticle ) )
                    {
                        articles.Add( currentArticle );
                    }
                    
                }

            }
            foreach( BlogArticle article in articles )
            {
                currentItem.Id = article.Id;
                if( !items.Contains( currentItem ) )
                {
                    article.DestroyPublishedInfo();
                    articles.Remove( article );
                }

            }
            

        }

        

        

    }
}
