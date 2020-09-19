using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class UsersDTO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("User_Id")]
        public int User_Id { get; set; }
        [BsonElement("User_Login")]
        public string User_Login { get; set; }
        [BsonElement("User_Password")]
        public string User_Password { get; set; }
        [BsonElement("User_Name")]
        public string User_Name { get; set; }
        [BsonElement("User_Last_Name")]
        public string User_Last_Name { get; set; }
        [BsonElement("Interests")]
        public List<string> Interests { get; set; }
        [BsonElement("Friends_Ids")]

        public List<int> Friends_Ids { get; set; }

        [BsonElement("Reg_Date")]

        //[BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
       public string Reg_Date { get; set; }

    }
}
