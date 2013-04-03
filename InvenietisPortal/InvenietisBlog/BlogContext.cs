using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog
{
    public class BlogContext
    {
        string _path;
        bool _isDirty;

        public BlogContext(string path)
        {
            _path = path;
        }

        static public BlogContext Load(string path)
        {
            return new BlogContext( path );
        }



        public string CurrentPath { get { return _path; } }

        public bool IsDirty { get { return _isDirty; } }

        public IReadOnlyList<BlogSource> BlogSource { get { return null; } }

        public BlogSource CreateBlogSource()
        {
            var b = new BlogSource(this);
            //TODO: Add to list...
            return b;
        }

        public void Save(string path = null)
        {
            if (path == null) path = _path;
            // TODO: save...
            _path = path;
            _isDirty = false;
        }

        internal void SetDirty()
        {
            _isDirty = true;
        }
    }
}