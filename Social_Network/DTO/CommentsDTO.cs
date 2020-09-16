using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class CommentsDTO
    {
        public int Comment_Id { get; set; }
        public int Author_Id { get; set; }
        public int Comment_Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        [BsonElement("Create_Date")]
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime Create { get; set; }

        [BsonElement("Modify_Date")]
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime Modify { get; set; }

    }
}
