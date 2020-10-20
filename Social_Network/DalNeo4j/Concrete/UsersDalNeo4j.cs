using DalNeo4j.Interfaces;
using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClientRepository;
using Neo4jClient;
using Neo4j;
using DTONeo4j.Relationships;
using Neo4jClient.Cypher;

namespace DalNeo4j.Concrete
{
    public class UsersDalNeo4j : IUsersDalNeo4j
    {
        private string connectionString;
        private string login;
        private string pass;
        public UsersDalNeo4j(string connectionString,string login,string pass)
        {
            this.connectionString = connectionString;
            this.login = login;
            this.pass = pass;
        }
        public void AddRelationship(UserLableDTO u1, UserLableDTO u2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User),(user2:User)")
                    .Where("user1.User_Id = {p_id1}")
                    .AndWhere("user2.User_Id = {p_id2}")
                    .WithParam("p_id1", u1.User_Id)
                    .WithParam("p_id2", u2.User_Id)
                    .Create("(user1)-[:Friends]->(user2)")
                    .ExecuteWithoutResults();
            }
        }
        public void AddRelationship(int u1_id, int u2_id)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User),(user2:User)")
                    .Where("user1.User_Id = {p_id1}")
                    .AndWhere("user2.User_Id = {p_id2}")
                    .WithParam("p_id1", u1_id)
                    .WithParam("p_id2", u2_id)
                    .Create("(user1)-[:Friends]->(user2)")
                    .ExecuteWithoutResults();
            }
        }

        public void AddUser(UserLableDTO u)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
            
                client.Cypher.Create("(u:User { User_Id: {p1},UserLogin: {p2},FisrtName: {p3},LastName: {p4} })")
                    .WithParam("p1",u.User_Id)
                    .WithParam("p2",u.User_Login)
                    .WithParam("p3", u.User_Name)
                    .WithParam("p4", u.User_Last_Name)
                    .ExecuteWithoutResults();
            }
        }

        public void DeleteRelationship(UserLableDTO u1, UserLableDTO u2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Friends]-(user2:User)")
                    .Where("user1.User_Id = {p_id1}")
                    .AndWhere("user2.User_Id = {p_id2}")
                    .WithParam("p_id1", u1.User_Id)
                    .WithParam("p_id2", u2.User_Id)
                    .Delete("r")
                    .ExecuteWithoutResults();

            }
        }
        public void DeleteRelationship(int u1_id, int u2_id)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Friends]-(user2:User)")
                    .Where("user1.User_Id = {p_id1}")
                    .AndWhere("user2.User_Id = {p_id2}")
                    .WithParam("p_id1", u1_id)
                    .WithParam("p_id2", u2_id)
                    .Delete("r")
                    .ExecuteWithoutResults();
            }
        }

        public void DeleteUser(UserLableDTO u)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Friends]-()")
                    .Where("user1.User_Id = {p_id}")
                    .WithParam("p_id", u.User_Id)
                    .Delete("r,user1")
                    .ExecuteWithoutResults();

            }
        }

        public UserLableDTO GetUser(int id)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                var user = client.Cypher
                    .Match("(u:User)")
                    .Where((UserLableDTO u) => u.User_Id == id)
                    .Return(u => u.As<UserLableDTO>())
                    .Results;
                UserLableDTO to_ret = new UserLableDTO() { User_Id = id };
                foreach (var u in user)
                {
                    to_ret.User_Id = u.User_Id;
                    to_ret.User_Login = u.User_Login;
                    to_ret.User_Name = u.User_Name;
                    to_ret.User_Last_Name = u.User_Last_Name;
                }
                return to_ret;


            }
        }

        public bool HasRelationship(UserLableDTO u1, UserLableDTO u2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                var is_friends = client.Cypher
                   .Match("(user1:User)-[r:Friends]-(user2:User)")
                   .Where((UserLableDTO user1) => user1.User_Id == u1.User_Id)
                   .AndWhere((UserLableDTO user2) => user2.User_Id == u2.User_Id)
                   .Return(r => r.As<Friends>()).Results; // how to know type of relationship?
                if(is_friends.Count() > 0)
                {
                    return true;
                }
                return false;

            }
        }

        public int MinPathBetween(UserLableDTO u1, UserLableDTO u2)
        {
            return this.MinPathBetween(u1.User_Id, u2.User_Id);
        }

        public int MinPathBetween(int id1, int id2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                var res = client.Cypher
                    .Match("(u1:User{User_Id: {p_id1} }),(u2:User{User_Id: {p_id2} })," +
                    " p = shortestPath((u1)-[:Friends*]-(u2))")
                    .WithParam("p_id1", id1)
                    .WithParam("p_id2", id2)
                    .Return(ret => ret.As<Result>())
                    .Results;
                int path_len =-1;
                foreach(var t in res)
                {
                    path_len =  Convert.ToInt32(t.length);
                }
                return path_len;
            }
        }

        public List<UserLableDTO> MinPathBetweenList(int id1, int id2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                if(id1 == id2) { return new List<UserLableDTO>(); ; }
                client.Connect();
                var res = client.Cypher
                    .Match("(u1:User{User_Id: {p_id1} }),(u2:User{User_Id: {p_id2} })," +
                    " p = shortestPath((u1)-[:Friends*]-(u2))")
                    .WithParam("p_id1", id1)
                    .WithParam("p_id2", id2)
                    .Return((ret,len) => new
                    {
                        shortestPath = Neo4jClient.Cypher.Return.As<IEnumerable<Node<UserLableDTO>>>("nodes(p)")
                    })
                    .Results;
                List<UserLableDTO> to_ret = new List<UserLableDTO>();
                foreach(var item in res)
                {
                    foreach(var it in item.shortestPath.ToList())
                    {
                        to_ret.Add(it.Data);
                    }
                }
                return to_ret;
            }
        }
    }
}
