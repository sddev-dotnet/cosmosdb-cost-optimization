using Demo.CosmosDB_Final.Models.Teams;
using Demo.CosmosDB_Final.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Accounts.Users.Coaches
{
    public class CoachAssignment : BaseEntity
    {
        public Guid CoachId { get; set; }
        
        public TeamOverview TeamOverview { get; set; }

        public CoachLevel Level { get; set; }

        public TeamLevel TeamLevel { get; set; }

        public override string PartitionKey => CoachId.ToString(); 
    }
}
