using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.Repositories.Queries.Filters
{
    public class OrderFilter
    {
        public string OrderBy { get; set; }

        public bool OrderDesc { get; set; }
    }
}
