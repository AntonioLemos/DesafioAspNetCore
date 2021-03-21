using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public interface ILocalStorageService
    {
        Task<User> GetItem(string key);
        Task SetItem(string key, User value);
        Task RemoveItem(string key);
    }
}
