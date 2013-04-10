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
        BlogContext _ctx;

        public BlogRefreshResult()
        {
            _refreshTime = new DateTime();
            _errorMessage = null;
            _newArticleCount = 0;
            _disappearedArticleCount = 0;
            _ctx = new BlogContext();
        }
        public DateTime RefreshTime 
        { 
            get { return _refreshTime; }
            set
            {
                _refreshTime = DateTime.UtcNow;
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
    }
}
