using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.Interfaces
{
    public interface IStreamManager
    {
        List<PostDTO> GetStreamForUser(long id);
    }
}
