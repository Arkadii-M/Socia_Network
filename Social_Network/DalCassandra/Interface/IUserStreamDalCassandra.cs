using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCassandra.Interface
{
    public interface IUserStreamDalCassandra
    {
        UserStreamDTO GetStreamForUser(long id, int LIMIT);

        void AddPostToUsersStreams(PostDTO post, List<long> UsersIds);
        void AddPostToUsersStreams(PostDTO post, List<string> UsersLogins);

        void UpdateStreamPost(PostDTO post, List<long> UsersIds);
    }
}
