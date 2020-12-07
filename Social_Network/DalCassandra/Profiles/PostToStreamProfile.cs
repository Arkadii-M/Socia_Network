using AutoMapper;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCassandra.Profiles
{
    public class PostToStreamProfile:Profile
    {
        public PostToStreamProfile()
        {
            CreateMap<StreamDTO, PostDTO>().ReverseMap();
        }
    }
}
