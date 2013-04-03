using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using CK.Core;

namespace Invenietis.Blog
{
    public class BlogContext
    {
        readonly List<BlogSource> _sources;
        readonly CK.Core.IReadOnlyList<BlogSource> _sourcesEx;
        string _path;
        bool _isDirty;

        public BlogContext( string path = null )
        {
            _path = path;
            _sources = new List<BlogSource>();
            _sourcesEx = new ReadOnlyListOnIList<BlogSource>( _sources );
        }

        static public BlogContext Load( string path )
        {
            // TODO.
            return new BlogContext( path );
        }

        public string CurrentPath { get { return _path; } }

        public bool IsDirty { get { return _isDirty; } }

        public CK.Core.IReadOnlyList<BlogSource> BlogSource { get { return _sourcesEx; } }

        public BlogSource CreateBlogSource()
        {
            var s = new BlogSource( this );
            _sources.Add( s );
            return s;
        }

        internal void OnDestroyBlogSource( BlogSource s )
        {
            Debug.Assert( s.Context == this );
            _sources.Remove( s );
            _isDirty = true;
        }

        public void Save( string path = null )
        {
            if( path == null ) path = _path;
            if( path == null ) throw new ArgumentException( "path" );


            _path = path;
            _isDirty = false;
        }

        internal void SetDirty()
        {
            _isDirty = true;
        }
    }
}