using BuisnesLogic.Interfaces;
using DalCassandra.Concrete;
using DalCassandra.Interface;
using DalNeo4j.Interfaces;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.Concrete
{
    public class StreamManager: IStreamManager
    {

        private readonly IUserStreamDalCassandra _userStreamDal;
        private readonly IUsersDalNeo4j _usersDalNeo4J;
        public StreamManager()
        {
            _usersDalNeo4J = null;
            _userStreamDal = null;
        }
        public StreamManager(IUserStreamDalCassandra userStreamDal,IUsersDalNeo4j usersDalNeo4J)
        {
            _userStreamDal = userStreamDal;
            _usersDalNeo4J = usersDalNeo4J;
        }

        public List<PostDTO> GetStreamForUser(long id)
        {
           var stream = this._userStreamDal.GetStreamForUser(id, 20);
           return stream.Stream;
        }
    }
}
