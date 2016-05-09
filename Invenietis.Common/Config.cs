using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;
using Invenietis.LocalizedRoutes.Config;

namespace Invenietis.Common
{
    public class Config
    {
        public string DatabasePath { get; set; }

        public CultureConfig Cultures { get; set; }
    }
}
