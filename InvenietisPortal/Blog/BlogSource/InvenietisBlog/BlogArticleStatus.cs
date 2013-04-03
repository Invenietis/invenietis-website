using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog
{
    public enum BlogArticleStatus
    {
        None = 0,
        New = 1,
        Published = 2,
        Rejected = 3,
        Hidden = 4
    }

}