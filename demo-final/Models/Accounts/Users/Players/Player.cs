using Demo.CosmosDB_Final.Models.Utilities.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Accounts.Users.Players
{
    public class Player : User
    {
        public List<AssociatedItem> AssociatedItems { get; set; }

        public string Position { get; set; }

        public List<PlayerAssignment> TeamAssignments { get; set; } = new();
    }
}
