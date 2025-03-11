using Demo.CosmosDB_Final.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Accounts.Users
{
    public class User : BaseEntity
    {
        public Guid AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Address Address { get; set; }

        public override IList<string> ItemType => [ GetType().Name, "User"];

        public override string PartitionKey => AccountId.ToString();
    }
}
