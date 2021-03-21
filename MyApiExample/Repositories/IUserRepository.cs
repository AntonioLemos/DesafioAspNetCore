using MyApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApiExample.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomeUser"></param>
        /// <param name="documentUser"></param>
        /// <returns></returns>
        User Login(string nomeUser, string documentUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<User> GetUsers(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        User GetUserbyDocument(string documento);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        User CreateEditUser(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        void DeleteUsers(User user);

    }
}
