using System;

namespace Invenietis.LocalizedRoutes.Mvc
{
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false )]
    public class LocalizedRoutesAttribute : Attribute
    {
        public LocalizedRoutesAttribute()
        {

        }
    }
}
