using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace Invenietis.Blog
{
    [Serializable]
    public class BlogArticle
    {
        internal DateTime _creationDate;
        internal Uri _uri;
        internal BlogArticleStatus _status;
        internal BlogArticlePublished _published;
        internal DateTimeOffset _lastModificationDate;
        internal string _originalTitle;
        internal BlogSource _source;

        /// <summary>
        /// One article could be written by many authors.
        /// </summary>
        //public Collection<Author> Authors = new Collection<Author>();

        public BlogSource Source 
        {
            get { return _source; }
            set
            {
                if( _source == null ) throw new ArgumentNullException();
            }
        }

        public Uri Uri 
        {
            get { return _uri; }
            set
            {
                if( value != _uri )
                {
                    _uri = value;
                    _source.SetDirty();
                }
            }
        }

        /// <summary>
        /// Gets the creation date of the original article (extracted from the feed).
        /// </summary>
        public DateTime CreationDate 
        {
            get { return _creationDate; }
            set
            {
                if( value != _creationDate )
                {
                    _creationDate = value;
                    _source.SetDirty();
                }
            }
        }

        /// <summary>
        /// Gets the modification date of the original article (extracted from the feed).
        /// </summary>
        public DateTimeOffset LastModificationDate 
        {
            get { return _lastModificationDate; }
            set
            {
                if( value != _lastModificationDate )
                {
                    _lastModificationDate = value;
                    _source.SetDirty();
                }
            }
        }

        /// <summary>
        /// Gets the original title of the article (extracted from the feed).
        /// </summary>
        public string OriginalTitle 
        {
            get { return _originalTitle; }
            set
            {
                if( value != _originalTitle )
                {
                    _originalTitle = value;
                    _source.SetDirty();
                }
            }
        }

        public BlogArticleStatus Status 
        {
            get { return _status; }
            set 
            {
                if (value == BlogArticleStatus.None) throw new ArgumentException();
                if (value == BlogArticleStatus.Published && _published == null) throw new InvalidOperationException("Cannot publish an inexisting published information. You must call EnsurePublishedInfo() first.");
                _status = value;
            }
        }

        /// <summary>
        /// Gets the published info if it has been created once.
        /// Null if <see cref="EnsurePublishedInfo"/> has never been called.
        /// </summary>
        public BlogArticlePublished PublishedInfo { get { return _published; } }

        public BlogArticlePublished EnsurePublishedInfo()
        {
            if (_published == null)
            {
                _published = new BlogArticlePublished(this);
            }
            return _published;
        }
        
        public void DestroyPublishedInfo()
        {
            _published = null;
            Status = BlogArticleStatus.Rejected;
        }

             
    }
}