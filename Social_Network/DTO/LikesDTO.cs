using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LikesDTO
    {
        [BsonElement("User_Id")]
        public int User_Id { get; set; }
    }
}
