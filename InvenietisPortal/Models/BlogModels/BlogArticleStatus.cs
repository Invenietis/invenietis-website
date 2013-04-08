using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invenietis.Blog
{
    public enum BlogArticleStatus
    {
        None = 0,
        New = 1,
        Published = 2,
        Rejected = 3,
        

        HiddenByEditor = 4,

        /// <summary>
        /// Whether the article disappeared from the <see cref="BlogSource"/>.
        /// </summary>
        HiddenByAuthor = 5,
    }

}