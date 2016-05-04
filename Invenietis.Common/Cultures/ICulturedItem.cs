using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invenietis.Common.Cultures
{
    public interface ICulturedItem
    {
        Dictionary<string, string> Cultures { get; set; }
    }
}
