using Demo.CosmosDB_Final.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Accounts.Users.Players
{
    /// <summary>
    /// Represents the assignment of a player to a team.
    /// </summary>
    /// <remarks>Does not inherit from BaseEntity because this is embedded</remarks>
    public class PlayerAssignment 
    {
        public TeamOverview TeamOverview { get; set; }
        public required string PlayerNumber { get; set; }

        
    }
}
