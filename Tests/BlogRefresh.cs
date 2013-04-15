using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invenietis.Blog;
using NUnit.Framework;
namespace Tests
{
    [TestFixture]
    public class BlogRefresh
    {
        BlogRefreshResult BlogRefreshResult = new BlogRefreshResult();
        
        static BlogContext ctx = new BlogContext( TestHelper.BasePath);
        BlogSource Source = ctx.CreateBlogSource();
        //int NewArticleCount;

        [Test]
        public void CreateBlogFromFeed()
        {
            Source.CreateBlog( new Uri( "http://macdarwin.github.com/atom.xml" ) );
            Assert.That( Source.AuthorName == "Guillaume Fradet" , "name "+Source.AuthorName);
           
        }
        [Test]
        public void LoadLastRefresh()
        {
            BlogRefreshResult = Source.RefreshFromUri( new Uri( "http://macdarwin.github.com/atom.xml" ) );
            Assert.That( BlogRefreshResult.IsSuccess );
            Assert.That( BlogRefreshResult.DisappearedArticleCount == 0, BlogRefreshResult.DisappearedArticleCount.ToString() );
            Assert.That( BlogRefreshResult.NewArticleCount != 0, BlogRefreshResult.NewArticleCount.ToString() );
            Assert.That( Source.Articles.Count == 3);
        }

        [Test]
        public void SaveLastSuccessUpdate()
        {
            Source.SaveContext( new Uri( "http://macdarwin.github.com/atom.xml" ), Path.Combine( TestHelper.BasePath, "BlogContextNew" ) );
            Assert.That( Source.SuccessfulUpdate );

        }
    }
}
