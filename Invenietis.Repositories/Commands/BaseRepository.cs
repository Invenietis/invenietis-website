using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;

namespace Invenietis.Repositories.Commands
{
    public class BaseRepository
    {
        public BaseRepository( CultureProvider cultureProvider )
        {
            CultureProvider = cultureProvider;
        }

        public CultureProvider CultureProvider { get; private set; }
    }
}
