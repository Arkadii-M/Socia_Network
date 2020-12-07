using DTOCassandra.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOCassandra
{
    public class StreamDTO
    {

        public long User_Id { get; set; }
        public Guid Post_Id { get; set; }
        public long Author_Id { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public List<long> Likes { get; set; }
        public List<long> Dislikes { get; set; }
        public DateTimeOffset Create_Date { get; set; }
        public DateTimeOffset Modify_Date { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
