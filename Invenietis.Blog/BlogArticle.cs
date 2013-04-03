using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace Blog
{
   
    public class BlogArticle
    {

        BlogArticleStatus _status;
        BlogArticlePublished _published;

        /// <summary>
        /// One article could be written by many authors.
        /// </summary>
        //public Collection<Author> Authors = new Collection<Author>();

        public BlogSource Source { get; set; }

        public Uri Uri { get; set; }

        /// <summary>
        /// Gets the creation date of the original article (extracted from the feed).
        /// </summary>
        public DateTimeOffset CreationDate { get; set; }

        /// <summary>
        /// Gets the modification date of the original article (extracted from the feed).
        /// </summary>
        public DateTimeOffset LastModificationDate { get; set; }

        /// <summary>
        /// Gets the original title of the article (extracted from the feed).
        /// </summary>
        public string OriginalTitle { get; set; }

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