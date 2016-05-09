using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.Repositories.Queries.Filters
{
    public class ProjectFilter
    {
        public int CategoryId { get; set; }

        public int ClientId { get; set; }

        public bool? Published { get; set; }
    }
}
