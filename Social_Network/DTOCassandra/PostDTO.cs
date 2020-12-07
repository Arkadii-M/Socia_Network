using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;
using DTOCassandra.UDT;

namespace DTOCassandra
{
    [TableName("posts")]
    [PrimaryKey("Post_Id")]
    public class PostDTO
    {
        [PartitionKey]
        [Column("Post_Id")]
        public Guid Post_Id { get; set; }

        [Column("Author_Id")]
        public long Author_Id { get; set; }

        [Column("Body")]
        public string Body { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Likes")]
        public List<long> Likes { get; set; }

        [Column("Dislikes")]
        public List<long> Dislikes { get; set; }

        [Column("Create_Date")]
        public DateTimeOffset Create_Date { get; set; }


        [Column("Modify_Date")]
        public DateTimeOffset Modify_Date { get; set; }

        [Column("Comments")]
        [Cassandra.Mapping.Attributes.FrozenValue]
        public List<Comment> Comments { get; set; }
    }
}
