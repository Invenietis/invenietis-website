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
        readonly BlogContext _context;
        readonly List<BlogArticle> _articles;
        readonly CK.Core.IReadOnlyList<BlogArticle> _articlesEx;

        string _blogTitleFR;
        string _blogTitleEN;
        Uri _rssUri;
        bool _hidden;
        bool _isDirty;

        internal BlogSource( BlogContext context )
        {
            Debug.Assert( context != null );
            _context = context;
            _articles = new List<BlogArticle>();
            _articlesEx = new ReadOnlyListOnIList<BlogArticle>( _articles );
        }

        public BlogContext Context { get { return _context; } }

        public void Destroy()
        {
            _context.OnDestroyBlogSource( this );
        }

        public bool Hidden 
        { 
            get { return _hidden; }
            set
            {
                if( _hidden != value )
                {
                    _hidden = value;
                    SetDirty();
                }
            }
        }

        public Uri RSSUri { get; set; }

        public string BlogTitleFR
        {
            get { return _blogTitleFR; }
            set
            {
                if( value != _blogTitleFR )
                {
                    _blogTitleFR = value;
                    SetDirty();
                }
            }
        }

        public string BlogHtmlDescriptionFR { get; set; }

        public string BlogTitleEN { get; set; }

        public string BlogHtmlDescriptionEN { get; set; }

        public BlogLanguage BlogLanguage { get; set; }

        public string AuthorUri { get; set; }

        public string AuthorName { get; set; }

        public string AuthorEMail { get; set; }

        public CK.Core.IReadOnlyList<BlogArticle> Articles { get { return _articlesEx; } }

        internal void SetDirty( [CallerMemberName] string memberName = null )
        {
            _isDirty = true;
            _context.SetDirty();
        }
    }
}