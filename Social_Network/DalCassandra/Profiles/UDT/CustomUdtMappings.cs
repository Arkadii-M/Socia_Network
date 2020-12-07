using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCassandra.Profiles
{
    public abstract class CustomUdtMappings
    {
        private readonly IDictionary<Type, UdtMap> _definitions = new Dictionary<Type, UdtMap>();

        public UdtMap[] Definitions => _definitions.Values.ToArray();

        public UdtMap<TPoco> For<TPoco>(string udtName = null, string keyspace = null) where TPoco : new()
        {
            if (_definitions.TryGetValue(typeof(TPoco), out var map) == false)
            {
                map = UdtMap.For<TPoco>(udtName, keyspace);
                _definitions.Add(typeof(TPoco), map);
            }

            return (UdtMap<TPoco>)map;
        }
    }
}
