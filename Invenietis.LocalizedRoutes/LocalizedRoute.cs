using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.LocalizedRoutes
{
    public class LocalizedRoute : ILocalizedRoute
    {
        public string Culture { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Template { get; set; }

        public bool IsDefault { get; set; }
    }
}
