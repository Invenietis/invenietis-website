using System;
using System.Collections.Generic;
using System.Linq;
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

        

    }
}
