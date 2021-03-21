using MyApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApiExample.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        dynamic Authenticate(User model);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        dynamic GetUsers(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        dynamic DeleteUsers(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        dynamic CreateEditUser(User user);

    }
}
