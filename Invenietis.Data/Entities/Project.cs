using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;
using LiteDB;

namespace Invenietis.Data.Entities
{
    public class Project : ICulturedItem
    {
        public int ProjectId { get; set; }

        public DbRef<Client> Client { get; set; }

        public DbRef<ProjectCategory> Category { get; set; }

        public Dictionary<string, string> Cultures { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }

    public class ProjectCategory : ICulturedItem
    {
        public int ProjectCategoryId { get; set; }

        public Dictionary<string, string> Cultures { get; set; }
    }
}
