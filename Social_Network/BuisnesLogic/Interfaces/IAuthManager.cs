using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.Interfaces
{
    public interface IAuthManager
    {
        bool Login(string UName,string UPass);
    }
}
