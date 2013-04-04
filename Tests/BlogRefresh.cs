using System;
using System.Collections.Generic;
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
        static string _path;
        static BlogContext ctx = new BlogContext( _path = null);
        BlogSource Source = ctx.CreateBlogSource();
        //int NewArticleCount;
        [Test]
        public void Load()
        {
            BlogRefreshResult = Source.RefreshFromUri( new Uri( "http://macdarwin.github.com/atom.xml" ) );
            Assert.That( BlogRefreshResult.IsSuccess );
            Assert.That( BlogRefreshResult.DisappearedArticleCount != 0, BlogRefreshResult.DisappearedArticleCount.ToString() );
            Assert.That( BlogRefreshResult.NewArticleCount != 0, BlogRefreshResult.NewArticleCount.ToString() );  
        }
    }
}
