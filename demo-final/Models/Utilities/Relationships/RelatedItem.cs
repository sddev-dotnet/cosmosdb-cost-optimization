using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Utilities.Relationships
{
    public class RelatedItem : BaseEntity
    {
        public Guid? TargetId { get; set; }

        public string TargetType { get; set; }

        public Guid? SourceId { get; set; }

        public string SourceType { get; set; }
    }
}
