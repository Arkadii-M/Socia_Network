using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOCassandra.UDT
{

    public class Comment
    {
        public long User_Id { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Create_Date { get; set; }
        public DateTimeOffset Modify_Date { get; set; }

    }
}
