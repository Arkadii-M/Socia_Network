using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOCassandra;

namespace DalCassandra.Profiles
{
    public class StreamProfile : Mappings
    {
        public StreamProfile()
        {
            For<StreamDTO>()
                 .TableName("stream")
                 .PartitionKey(u => u.User_Id)
                 .ClusteringKey(u => u.Post_Id)
                 .CaseSensitive()
                 .Column(u => u.User_Id, cm => cm.WithName("User_Id"))
                 .Column(u => u.Post_Id, cm => cm.WithName("Post_Id"))
                 .Column(u => u.Author_Id, cm => cm.WithName("Author_Id"))
                 .Column(u => u.Title, cm => cm.WithName("Title"))
                 .Column(u => u.Body, cm => cm.WithName("Body"))
                 .Column(u => u.Likes, cm => cm.WithName("Likes"))
                 .Column(u => u.Create_Date, cm => cm.WithName("Create_Date"))
                 .Column(u => u.Modify_Date, cm => cm.WithName("Modify_Date"))
                 .Column(u => u.Comments, cm => cm.WithName("Comments"))
                 .Column(u => u.Comments, cm => cm.WithFrozenValue());

        }
    }
}
