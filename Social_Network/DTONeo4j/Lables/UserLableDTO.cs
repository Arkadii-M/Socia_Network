using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTONeo4j
{
    public class UserLableDTO
    {
        [JsonProperty(PropertyName = "User_Id")]
        public int User_Id { get; set; }
        [JsonProperty(PropertyName = "UserLogin")]
        public string User_Login { get; set; }

        [JsonProperty(PropertyName = "FisrtName")]
        public string User_Name { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string User_Last_Name { get; set; }
    }
}
