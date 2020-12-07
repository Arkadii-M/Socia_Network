using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOCassandra
{
    public class UserStreamDTO
    {
        public long User_Id { get; set; }

        public List<PostDTO> Stream { get; set; }
    }
}
