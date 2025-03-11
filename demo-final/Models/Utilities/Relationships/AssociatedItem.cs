using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Utilities.Relationships
{
    public class AssociatedItem 
    {
        public Guid? TargetId { get; set; }

        public string TargetType { get; set; }

        public string RelationshipType { get; set; }

        public string DisplayName { get; set; }
    }
}
