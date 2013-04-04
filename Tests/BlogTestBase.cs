using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CK.Core;

namespace Tests
{
    public class BlogTestBase
    {
        string _basePath = "../../Context";
        public string BasePath 
        { get {return _basePath;} }
    }

    public static class TestHelper
    {
        static string _basePath = "../../Context";
        public static string BasePath
        { get { return _basePath; } }
    }
}
