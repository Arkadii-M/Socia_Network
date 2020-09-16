using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class UsersDTO
    {
        public int User_Id { get; set; }
        public string User_Login { get; set; }
        public string User_Password { get; set; }
        public string User_Name { get; set; }
        public string User_Last_Name { get; set; }
        public List<string> Interests { get; set; }

        public List<int> Friends_Ids { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime Reg_Date { get; set; }

    }
}
