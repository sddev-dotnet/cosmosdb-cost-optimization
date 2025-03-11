using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Utilities
{
    public abstract class BaseEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("ttl")]
        public virtual int TimeToLive { get; set; } = -1;

        /// <summary>
        /// A type name collection that is used to facilitate searching
        /// </summary>
        public virtual IList<string> ItemType => new List<string>() { this.GetType().Name };

        public virtual string PartitionKey => this.GetType().Name;
    }
}
