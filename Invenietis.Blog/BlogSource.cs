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
    [Serializable]
    public partial class BlogSource
    {
        readonly BlogContext _context;
        readonly List<BlogArticle> _articles;

        [NonSerialized]
        readonly CK.Core.IReadOnlyList<BlogArticle> _articlesEx;
        string _id;
        string _blogHtmlDescriptionFR;
        string _blogHtmlDescriptionEN;
        string _blogTitleFR;
        string _blogTitleEN;
        string _authorName;
        string _authorUri;
        string _authorEMail;
        BlogLanguage _blogLanguage;
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
        
        public string Id 
        { 
            get {return _id;}
            set
            {
                if(_id != value)
                {
                    _id = value;
                    SetDirty();
                }
            }      
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

        public Uri RSSUri { 
            get { return _rssUri; }
            set
            {
                if( value != _rssUri )
                {
                    _rssUri = value;
                    SetDirty();
                }
            }
        }

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

        public string BlogHtmlDescriptionFR 
        {
            get { return _blogHtmlDescriptionFR; }
            set
            {
                if( value != _blogHtmlDescriptionFR )
                {
                    _blogHtmlDescriptionFR = value;
                    SetDirty();
                }
            }
        }

        public string BlogTitleEN 
        {
            get { return _blogTitleEN; }
            set 
            {
                if( value != _blogTitleEN )
                {
                    _blogTitleEN = value;
                    SetDirty();
                }
            }           
        }

        public string BlogHtmlDescriptionEN 
        {
            get { return _blogHtmlDescriptionEN; }
            set
            {
                if( value != _blogHtmlDescriptionEN )
                {
                    _blogHtmlDescriptionEN = value;
                    SetDirty();
                }
            }
        }

        public BlogLanguage BlogLanguage 
        {
            get { return _blogLanguage; }
            set
            {
                if( value != _blogLanguage ) 
                {
                    _blogLanguage = value;
                    SetDirty();
                }
               
            }
        }

        public string AuthorUri 
        {
            get { return _authorUri; }
            set
            {
                if( value != _authorUri )
                {
                    _authorUri = value;
                    SetDirty();
                }
            }
        }

        public string AuthorEMail
        {
            get { return _authorEMail; }
            set
            {
                if( value != _authorEMail )
                {
                    _authorEMail = value;
                    SetDirty();
                }
            }
        }

        public string AuthorName
        {
            get { return _authorName; }
            set
            {
                if( value != _authorName )
                {
                    _authorName = value;
                    SetDirty();
                }
            }
        }
        
        public CK.Core.IReadOnlyList<BlogArticle> Articles { get { return _articlesEx; } }

        internal void SetDirty( [CallerMemberName] string memberName = null )
        {
            _isDirty = true;
            _context.SetDirty();
        }

    }
}