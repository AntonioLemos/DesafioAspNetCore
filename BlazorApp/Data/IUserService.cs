using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public interface IUserService
    {
        Task<dynamic> Login(User user);

        Task<dynamic> GetUserLogon();

        Task<dynamic> GetUsers(User user);

        Task<dynamic> Create(User user);

        Task<dynamic> Edit(User user);

        Task<dynamic> Delete(int idUser);
    }
}
