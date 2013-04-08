using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Syndication;
using System.Xml;
using NUnit.Framework;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Invenietis.Blog;

namespace Tests
{
    [TestFixture]
    public class BlogRSSTest
    {
        [Test]
        public void ReadRSSFeed()
        {
            using( XmlReader reader1 = XmlReader.Create( "http://macdarwin.github.com/atom.xml" ) )
            {
                SyndicationFeed feed1 = SyndicationFeed.Load( reader1 );
                SyndicationPerson author1 = feed1.Authors[0];

                Assert.That( feed1.Id != null, feed1.Id.ToString() );
                Assert.That( author1.Name == "Guillaume Fradet", author1.Name.ToString() );
            }

            using( XmlReader reader2 = XmlReader.Create( "http://cedricdotnet.blogspot.com/feeds/posts/default" ) )
            {
                SyndicationFeed feed2 = SyndicationFeed.Load( reader2 );
                SyndicationPerson author2 = feed2.Authors[0];

                Assert.That( feed2.Id != null, feed2.Id.ToString() );
                Assert.That( author2.Name == "Cedric Legendre", author2.Name.ToString() );
            }
        }

        [Test]
        public void SaveContext()
        {
            BlogContext _ctx = new BlogContext( TestHelper.BasePath );
            _ctx.Save( Path.Combine(TestHelper.BasePath,"BlogContext" ));
            //BlogContext.Load( TestHelper.BasePath );
        }

        [Test]
        public void LoadContext()
        {
            BlogContext context = BlogContext.Load( Path.Combine( TestHelper.BasePath, "BlogContext" ) );
            //BlogSource source = context.CreateBlogSource();
            //Assert.That( source.AuthorName == "Cedric Legendre");
        }
    }
}