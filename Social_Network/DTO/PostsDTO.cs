using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PostsDTO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        [BsonElement("Post_Id")]
        public int Post_Id { get; set; }
        [BsonElement("Author_Id")]
        public int Author_Id { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Body")]
        public string Body { get; set; }
        [BsonElement("Tags")]
        public List<string> Tags { get; set; }
        [BsonElement("Likes")]
        public int Likes { get; set; }
        [BsonElement("Dislikes")]
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
