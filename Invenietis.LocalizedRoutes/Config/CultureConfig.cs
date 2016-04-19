using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes.Config
{
    public class CultureConfig
    {
        /// <summary>
        /// The default culture to use
        /// </summary>
        public string DefaultCulture { get; set; } 

        /// <summary>
        /// The cultures supported by the application. The <see cref="DefaultCulture"/> must be part of the list.
        /// </summary>
        public string[] SupportedCultures { get; set; } 

        /// <summary>
        /// The available fallbacks to supported cultures
        /// </summary>
        public Dictionary<string, string[]> FallbackMap { get; set; } 
    }
}
