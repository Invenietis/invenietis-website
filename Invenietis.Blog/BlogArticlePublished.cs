using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invenietis.Blog
{
    public class BlogArticlePublished
    {
        readonly BlogArticle _article;
        readonly BlogSource _source;
        private DateTime _publicationDate;
        private string _titleFR;
        private string _titleEN;
        private string _htmlAbstractFR;
        private string _htmlAbstractEN;

        internal BlogArticlePublished(BlogArticle a)
        {
            _article = a;
        }

        internal BlogArticlePublished( BlogArticle a, BlogSource s )
        {
            _article = a;
            _source = s;
        }
        public BlogArticle Article { get { return _article; } }

        public bool IsHidden { get { return Article.Status != BlogArticleStatus.Published; } }

        public DateTime PublicationDate { 
            get { return _publicationDate; } 
            set 
            {
                if( value != _publicationDate )
                {
                    _publicationDate = value;
                    _source.SetDirty();
                }
            } 
        }

        public string TitleFR 
        {
            get { return _titleFR; }
            set 
            {
                if( value != _titleFR )
                {
                    _titleFR = value;
                    _source.SetDirty();
                }
            } 
        }

        public string TitleEN
        {
            get { return _titleEN; }
            set
            {
                if( value != _titleEN )
                {
                    _titleEN = value;
                    _source.SetDirty();
                }
            }
        }

        public string HtmlAbstractFR
        {
            get { return _htmlAbstractFR; }
            set
            {
                if( value != _htmlAbstractFR )
                {
                    _htmlAbstractFR = value;
                    _source.SetDirty();
                }
            }
        }

        public string HtmlAbstractEN
        {
            get { return _htmlAbstractEN; }
            set
            {
                if( value != _htmlAbstractEN )
                {
                    _htmlAbstractEN = value;
                    _source.SetDirty();
                }
            }
        }      
    }
}