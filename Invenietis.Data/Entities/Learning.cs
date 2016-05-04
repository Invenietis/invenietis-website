using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;
using LiteDB;

namespace Invenietis.Data.Entities
{
    public class Learning : ICulturedItem
    {
        public int LearningId { get; set; }

        public DbRef<LearningCategory> Category { get; set; }

        public Dictionary<string, string> Cultures { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }

    public class LearningCategory : ICulturedItem
    {
        public int LearningCategoryId { get; set; }

        public Dictionary<string, string> Cultures { get; set; }
    }
}
