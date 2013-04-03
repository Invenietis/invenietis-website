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
        BlogContext ctx = new BlogContext( _path );
        BlogSource Source;
        //int NewArticleCount;
        [Test]
        public void Load()
        {
            BlogRefreshResult = Source.RefreshFromUri( new Uri( "http://macdarwin.github.com/atom.xml" ) );
            Assert.That( BlogRefreshResult.IsSuccess );
            
        }
    }
}
