using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using CK.Core;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Blog
{
     [Serializable]
    public class BlogSource
    {
        readonly BlogContext _context;
        string _blogTitleFR;

        private CK.Core.IReadOnlyList<BlogArticle> _blogArticles;
       
        private List<BlogArticle> _articles;
        internal BlogSource(BlogContext context, IEnumerable<SyndicationItem> items)
        {
            _context = context;
            _articles = items as List<BlogArticle>;
        }

        public List<BlogArticle> NewArticles
        {
            get { return _articles; }
            set { NewArticles = _articles; }
        }
        internal BlogSource(BlogContext context)
        {
            _context = context;
         
        }

            
        public Uri RSSUri { get; set; }

        public string BlogTitleFR 
        { 
            get { return _blogTitleFR; }
            set
            {
                if (value != _blogTitleFR)
                {
                    _blogTitleFR = value;
                    _context.SetDirty();
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

        public CK.Core.IReadOnlyList<BlogArticle> Articles { get { return null; } set { Articles = _blogArticles; } }

        
    }
}