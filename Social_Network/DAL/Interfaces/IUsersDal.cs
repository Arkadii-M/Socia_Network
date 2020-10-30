using System;
using System.Collections.Generic;
using DTO;
namespace DAL.Interfaces
{
    public interface IUsersDal
    {
        UsersDTO GetUserById(int id);
        List<UsersDTO> GetAllUsers();
        UsersDTO UpdateUser(UsersDTO user);
        UsersDTO CreateUser(UsersDTO user);
        UsersDTO GetUserByLogin(string login);
        void DeleteUser(int id);
    }
}
