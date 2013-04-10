﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel.Syndication;
using System.Web;

namespace Invenietis.Blog
{
    [Serializable]
    public class BlogArticle
    {
        DateTime _creationDate;
        Uri _uri;
        BlogArticleStatus _status;
        BlogArticlePublished _published;
        DateTimeOffset _lastModificationDate;
        string _originalTitle;
        BlogSource _source;
        string _id;
        List<string> _contributors;

        internal BlogArticle(BlogSource s)
        {
            _creationDate = new DateTime();
            _published = new BlogArticlePublished( this );
            _lastModificationDate = new DateTimeOffset();
            _originalTitle = "";
            _source = s;
            _id = "";
        }

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

        public List<string> Contributors
        {
            get { return _contributors; }
            set
            {
                if( value == null ) throw new ArgumentException();
                if( value != _contributors )
                {
                    _contributors = value;
                    _source.SetDirty();
                }
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
                if( value == null ) throw new InvalidOperationException( "the author didnt't specified a title for this article" );
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
        
        public string Id 
        {
            get { return _id; }
            set
            {
                if(value != _id)
                {
                    _id = value;
                    _source.SetDirty();

                }
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