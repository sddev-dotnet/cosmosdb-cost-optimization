using Demo.CosmosDB_Final.Models.Teams;
using Demo.CosmosDB_Final.Models.Utilities;
using Demo.CosmosDB_Final.Models.Utilities.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Games
{
    public class Game : BaseEntity
    {
        public DateTime Date { get; set; }

        public Address Location { get; set; }

        public List<AssociatedItem> Players { get; set; }

        public TeamOverview TeamOverview { get; set; }

    }
}
