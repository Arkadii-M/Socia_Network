using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class PostsDTO
    {
        public int Post_Id { get; set; }
        public int Author_Id { get; set; }
        public int Title { get; set; }
        public int Body { get; set; }
        public List<string> Tags { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        [BsonElement("Comments")]
        public List<CommentsDTO> Comment_List { get; set; }
        [BsonElement("Create_Date")]
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime Create { get; set; }

        [BsonElement("Modify_Date")]
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime Modify { get; set; }
    }
}
