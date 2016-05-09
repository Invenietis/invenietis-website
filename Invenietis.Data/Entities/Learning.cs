using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;
using LiteDB;

namespace Invenietis.Data.Entities
{
    public class Learning
    {
        public Learning()
        {
            Cultures = new Dictionary<string, CulturedLearning>();
        }

        public int LearningId { get; set; }

        public bool Published { get; set;}

        public int CategoryId { get; set; }

        public DbRef<LearningCategory> Category { get; set; }

        public Dictionary<string, CulturedLearning> Cultures { get; set; }

        public int Duration { get; set; }

        public string Illustration { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }

    public class CulturedLearning
    {
        public CulturedLearning()
        {
            Features = new List<string>();
        }

        public string Presentation { get; set; }

        public List<string> Features { get; set; }
    }

    public class LearningCategory
    {
        public int LearningCategoryId { get; set; }

        public Dictionary<string, string> Cultures { get; set; }
    }
}
