using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyApiExample.Models;

namespace MyApiExample.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly IUserExtensions _userExtensions;
        public UserRepository(IUserExtensions userExtensions)
        {
            _userExtensions = userExtensions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomeUser"></param>
        /// <param name="documentUser"></param>
        /// <returns></returns>
        public User Login(string nomeUser, string documentUser)
        {
            try
            {
                return _userExtensions.GetUser().Where(x => x.NomeUser.ToUpper() == nomeUser.ToUpper() && x.DocumentUser == documentUser).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers(User user)
        {
   
            try
            {
                List<User> users = new List<User>();
                if (user.Id != 0 && string.IsNullOrEmpty(user.Uf))
                {
                    users.Add(GetUserById(user.Id));
                    return users;
                }

                if (user.Id == 0 && !string.IsNullOrEmpty(user.Uf))
                    return GetUserByUf(user.Uf);

                if (user.Id != 0 && !string.IsNullOrEmpty(user.Uf))
                {
                    users.Add(GetUserByIdAndUf(user.Id, user.Uf));
                    return users;
                }

                return _userExtensions.GetUser();
            }
            catch
            {
                throw;
            }


        }

        public User GetUserbyDocument(string documento)
        {

            try
            {
                return _userExtensions.GetUser().Where(x => x.DocumentUser == documento).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public User CreateEditUser(User user)
        {
            _userExtensions.Create(user);
            return user;
        }

        public void DeleteUsers(User user)
        {
            _userExtensions.Delete(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="uf"></param>
        /// <returns></returns>
        private User GetUserByIdAndUf(int userId, string uf)
        {
            return _userExtensions.GetUser().Where(x => x.Id == userId && x.Uf == uf).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private User GetUserById(int userId)
        {
            return _userExtensions.GetUser().Where(x => x.Id == userId).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ufId"></param>
        /// <returns></returns>

        private List<User> GetUserByUf(string ufId)
        {
            List<User> users = _userExtensions.GetUser();
            return users.Where(x => x.Uf.ToUpper() == ufId.ToUpper()).ToList();
        }
    }
}