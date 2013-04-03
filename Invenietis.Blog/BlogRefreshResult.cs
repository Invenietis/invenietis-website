using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invenietis.Blog
{
    /// <summary>
    /// Captures the work of <see cref="BlogSource.RefreshFromUri"/>.
    /// </summary>
    public class BlogRefreshResult
    {
        readonly DateTime _refreshTime;
        readonly string _errorMessage;
        readonly int _newArticleCount;
        readonly int _disappearedArticleCount;

        public DateTime RefreshTime { get { return _refreshTime; } }

        public bool IsSuccess { get { return _errorMessage == null; } }

        public string ErrorMessage { get { return _errorMessage; } }

        public int NewArticleCount { get { return _newArticleCount; } }

        public int DisappearedArticleCount { get { return _disappearedArticleCount; } }


    }
}
