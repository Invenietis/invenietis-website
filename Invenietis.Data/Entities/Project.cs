using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invenietis.Common.Cultures;
using LiteDB;

namespace Invenietis.Data.Entities
{
    public class Project
    {
        public Project()
        {
            Cultures = new Dictionary<string, CulturedProject>();
            Medias = new List<string>();
        }

        public int ProjectId { get; set; }

        public bool Published { get; set; }

        public int ClientId { get; set; }

        public DbRef<Client> Client { get; set; }

        public int CategoryId { get; set; }

        public DbRef<ProjectCategory> Category { get; set; }

        public Dictionary<string, CulturedProject> Cultures { get; set; }

        public DateTime ProjectDate { get; set; }

        public int Duration { get; set; }

        public List<string> Medias { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }

    public class CulturedProject
    {
        public CulturedProject()
        {
            Features = new List<string>();
        }

        public string Title { get; set; }

        public string Need { get; set; }

        public string Solution { get; set; }

        public List<string> Features { get; set; }

        public ProjectQuote Quote { get; set; }
    }

    public class ProjectQuote
    {
        public string Quote { get; set; }

        public string Author { get; set; }
    }

    public class ProjectCategory
    {
        public ProjectCategory()
        {
            Cultures = new Dictionary<string, string>();
        }

        public int ProjectCategoryId { get; set; }

        public Dictionary<string, string> Cultures { get; set; }
    }
}
