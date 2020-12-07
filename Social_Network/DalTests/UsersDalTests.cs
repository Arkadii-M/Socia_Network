using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Concrete;
using DTO;
using NUnit.Framework;
using System.EnterpriseServices;
using System.Linq;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;



namespace DalTests
{
    [TestFixture]
    public class UsersDalTests: ServicedComponent
    {
       // private string conn = "mongodb://localhost:27017/";

        public UsersDalTests()
        {

        }
        [Test]
        public void CreateUserTest()
        {
           UsersDTO test = new UsersDTO() { User_Id = 3,User_Login = "login3" };
           UsersDal some_dal = new UsersDal("mongodb://localhost:27017/");
           var User = some_dal.CreateUser(test); // 
           NUnit.Framework.Assert.IsTrue(User.User_Id > 0);
        }
    }
}
