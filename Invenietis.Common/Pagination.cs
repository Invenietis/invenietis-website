using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.Common
{
    public interface IPageInfo
    {
        int Page { get; }

        int PerPage { get; }
    }

    public interface IPageResult<out T> : IPageInfo
    {
        int Total { get; }

        IEnumerable<T> Models { get; }
    }

    public class PaginationInfo : IPageInfo
    {
        public int Page { get; set; }

        public int PerPage { get; set; }
    }

    public class PaginatedResult<T> : PaginationInfo, IPageResult<T>
    {
        public PaginatedResult( PaginationInfo paginationInfo, IEnumerable<T> models, int total )
        {
            Page = total > 0 ? paginationInfo.Page : -1;
            PerPage = paginationInfo.PerPage;
            Models = models;
            Total = total;
        }

        public int Total { get; private set; }

        public IEnumerable<T> Models { get; private set; }
    }
}
