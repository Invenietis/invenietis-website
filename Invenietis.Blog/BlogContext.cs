using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using CK.Core;

namespace Invenietis.Blog
{
    [Serializable]
    public class BlogContext
    {
        readonly List<BlogSource> _sources;
        [NonSerialized]
        readonly IReadOnlyList<BlogSource> _sourcesEx;
        private string _path;
        private bool _isDirty;

        public BlogContext( string path = null )
        {
            _path = path;
            _sources = new List<BlogSource>();
            _sourcesEx = new ReadOnlyListOnIList<BlogSource>( _sources );
        }

        public static BlogContext Load( string path )
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using(Stream stream = File.Open(path, FileMode.Open))
            {
                BlogSource blogData = (BlogSource)formatter.Deserialize( stream );
            }
            return new BlogContext( path );

        }

        public string CurrentPath { get { return _path; } }

        public bool IsDirty { get { return _isDirty; } }

        public IReadOnlyList<BlogSource> BlogSource { get { return _sourcesEx; } }
        
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

            BinaryFormatter formatter = new BinaryFormatter();
            using( Stream stream = File.Open( path, FileMode.Create ) )
            {
                BlogSource blogData = CreateBlogSource();
                
                formatter.Serialize( stream, blogData );
            }

            _path = path;
            _isDirty = false;
        }

        internal void SetDirty()
        {
            _isDirty = true;
        }
    }
}