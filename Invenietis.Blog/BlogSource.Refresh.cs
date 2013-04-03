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

namespace Invenietis.Blog
{
    public partial class BlogSource
    {
        BlogRefreshResult _lastRefreshResult;
        BlogRefreshResult _lastSuccessfulRefreshResult;

        public BlogRefreshResult LastRefreshResult { get { return _lastRefreshResult; } }

        public BlogRefreshResult LastSuccessfulRefreshResult { get { return _lastSuccessfulRefreshResult; } }

        /// <summary>
        /// Uses .Net 4.5 <see cref="SyndicationFeed"/> to retrieve changes from the source.
        /// </summary>
        /// <returns>An object that summarizes the update from the <see cref="RSSUri"/>.</returns>
        public BlogRefreshResult RefreshFromUri()
        {

        }
    }
}