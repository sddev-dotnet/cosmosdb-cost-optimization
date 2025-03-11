using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Utilities
{
    public class Address
    {
        public required string Address1 { get; set; }

        public string? Address2 { get; set; }

        public required string City { get; set; }

        public required string State { get; set; }

        public required string Zip { get; set; }
    }
}
