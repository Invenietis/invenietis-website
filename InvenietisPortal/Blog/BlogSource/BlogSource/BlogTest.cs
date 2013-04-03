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
using Blog;

namespace BlogTest
{
    [TestFixture]
    public class BlogTest
    {
        [Test]
        public void ReadRSSFeed()
        {
            using (XmlReader reader1 = XmlReader.Create("http://macdarwin.github.com/atom.xml"))
            {
                SyndicationFeed feed1 = SyndicationFeed.Load(reader1);
                SyndicationPerson author1 = feed1.Authors[0];

                Assert.That(feed1.Id != null, feed1.Id.ToString());
                Assert.That(author1.Name == "Guillaume Fradet", author1.Name.ToString());
            }

            using (XmlReader reader2 = XmlReader.Create("http://cedricdotnet.blogspot.com/feeds/posts/default"))
            {
                SyndicationFeed feed2 = SyndicationFeed.Load(reader2);
                SyndicationPerson author2 = feed2.Authors[0];
                
                Assert.That(feed2.Id != null, feed2.Id.ToString());
                Assert.That(author2.Name == "Cedric Legendre", author2.Name.ToString());
            }



            using (XmlReader reader3 = XmlReader.Create("http://blog.invenietis.com/syndication.axd"))
            {
                SyndicationFeed feed3 = SyndicationFeed.Load(reader3);
                SyndicationPerson author3 = feed3.Authors[0];
                Assert.That(feed3.Authors != null, feed3.Authors.Count.ToString());
                //foreach (SyndicationPerson author in feed3.Authors)
                //{
                //    Assert.That(author.Name == "Cedric Legendre", author.Name.ToString());
                //}
            }
            
        }

        [Test]
        public void SaveAndLoad()
        {
            string _path = "/Contexte";
            BlogContext _ctx = new BlogContext(_path);
            Stream stream = File.Open(_path, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            BlogSource blog = new BlogSource(_ctx);

            using (XmlReader reader = XmlReader.Create("http://macdarwin.github.com/atom.xml"))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                blog.RSSUri = feed.BaseUri;
                blog.AuthorEMail = feed.Authors[0].Email;
                blog.AuthorName = feed.Authors[0].Name;
                blog.AuthorUri = feed.Authors[0].Uri;
                if (feed.Language == "En")
                {
                    blog.BlogLanguage = BlogLanguage.English;
                    blog.BlogHtmlDescriptionEN = feed.Description.Text;
                    blog.BlogTitleEN = feed.Title.ToString();
                }
                else if (feed.Language == "Fr")
                {
                    blog.BlogLanguage = BlogLanguage.French;
                    blog.BlogHtmlDescriptionFR = feed.Description.Text;
                    blog.BlogTitleFR = feed.Title.ToString();
                }
                else
                {
                    blog.BlogLanguage = BlogLanguage.None;
                }


                blog.Articles = feed.Items as CK.Core.IReadOnlyList<BlogArticle>;
                foreach (BlogArticle article in blog.Articles)
                {
                    //article.CreationDate = feed.Items.GetEnumerator().Current.
                    article.LastModificationDate = feed.Items.GetEnumerator().Current.LastUpdatedTime;
                    article.OriginalTitle = feed.Items.GetEnumerator().Current.Title.Text;
                    if (article.Status == BlogArticleStatus.Published)
                    {
                        if(blog.BlogLanguage == BlogLanguage.English)
                        {
                            article.PublishedInfo.HtmlAbstractEN = feed.Items.GetEnumerator().Current.Summary.Text;
                            article.PublishedInfo.TitleEN = feed.Items.GetEnumerator().Current.Title.Text;
                        }
                        else if(blog.BlogLanguage == BlogLanguage.French)
                        {
                            article.PublishedInfo.HtmlAbstractFR = feed.Items.GetEnumerator().Current.Summary.Text;
                            article.PublishedInfo.TitleFR = feed.Items.GetEnumerator().Current.Title.Text;
                        }
                        //article.PublishedInfo.IsHidden;
                        article.PublishedInfo.PublicationDate = feed.Items.GetEnumerator().Current.PublishDate;

                    }
                    article.Source = blog;
                    if (article.CreationDate.Equals(TimeSpan.FromDays(8)))
                        article.Status = BlogArticleStatus.New;
                    article.Uri = feed.Items.GetEnumerator().Current.BaseUri;
                    feed.Items.GetEnumerator().MoveNext();

                }


            }

            formatter.Serialize(stream, blog);
            stream.Close();
            Assert.That(blog.RSSUri != null, blog.RSSUri.ToString());
           
        }

    }
}