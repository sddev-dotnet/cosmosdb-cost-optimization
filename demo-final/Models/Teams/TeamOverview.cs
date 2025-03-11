using Demo.CosmosDB_Final.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Teams
{
    public class TeamOverview : BaseEntity
    {
        public required string Name { get; set; }
    }
}
