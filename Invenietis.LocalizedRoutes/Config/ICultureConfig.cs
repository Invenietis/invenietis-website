using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes.Config
{
    public interface ICultureConfig
    {
        /// <summary>
        /// The default culture to use
        /// </summary>
        string DefaultCulture { get; } 

        /// <summary>
        /// The cultures supported by the application. The <see cref="DefaultCulture"/> must be part of the list.
        /// </summary>
        string[] SupportedCultures { get; } 

        /// <summary>
        /// The available fallbacks to supported cultures
        /// </summary>
        Dictionary<string, string[]> FallbackMap { get; } 
    }
}
