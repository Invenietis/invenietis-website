using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog
{
    public class BlogArticlePublished
    {
        readonly BlogArticle _article;
        private DateTimeOffset _publicationDate;
        private string _titleFR;
        private string _titleEN;
        private string _htmlAbstractFR;
        private string _htmlAbstractEN;

        internal BlogArticlePublished(BlogArticle a)
        {
            _article = a;
        }

        public BlogArticle Article { get { return _article; } }

        public bool IsHidden { get { return Article.Status != BlogArticleStatus.Published; } }

        public DateTimeOffset PublicationDate { get { return DateTime.Today; } set { PublicationDate = _publicationDate; } }

        public string TitleFR { get { return null; } set { TitleFR = _titleFR; } }

        public string TitleEN { get { return null; } set { TitleEN = _titleEN; } }

        public string HtmlAbstractFR { get { return null; } set { HtmlAbstractFR = _htmlAbstractFR; } }

        public string HtmlAbstractEN { get { return null; } set { HtmlAbstractEN = _htmlAbstractEN; } }

    }
}