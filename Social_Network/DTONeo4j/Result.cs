using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTONeo4j
{
    public class Result
    {
        [JsonProperty(PropertyName = "length")]
        public string length { get; set; }
    }
}
