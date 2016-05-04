using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;

namespace Invenietis.Data.Entities
{
    public class Client : ICulturedItem
    {
        public int ClientId { get; set; }

        public Dictionary<string, string> Cultures { get; set; }

        public string LogoPath { get; set; }
    }
}
