using Demo.CosmosDB_Final.Models.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CosmosDB_Final.Models.Teams
{
    public class Team : TeamOverview
    {
        
        [JsonConverter(typeof(StringEnumConverter))]
        public TeamLevel TeamLevel { get; set; }

        public Season Season { get; set; }

    }
}
