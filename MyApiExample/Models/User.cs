using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiExample.Repositories;
using MyApiExample.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApiExample.Models
{
    public class User
    {
        public int Id { get; set; }

        public string NomeUser { get; set; }

        public string DocumentUser { get; set; }
        public string Uf { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Token { get; set; }

    }

    public class UserExtensions : IUserExtensions
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService; 

        public UserExtensions(IHttpContextAccessor httpContextAccessor,ITokenService tokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        private const string UserSessionKey = "ListUser";

        public List<User> FirstTime()
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, NomeUser = "Antonio", DocumentUser = "12365478998", Uf = "MG", DataNascimento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) });
            users.Add(new User { Id = 2, NomeUser = "Joao", DocumentUser = "32198765489", Uf = "SP", DataNascimento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) });

            var userJson = JsonConvert.SerializeObject(users);

            _httpContextAccessor.HttpContext.Session.SetString(UserSessionKey, userJson);

            return JsonConvert.DeserializeObject<List<User>>(userJson);
        }

        public List<User> GetUser()
        {
            var value = _httpContextAccessor.HttpContext.Session.GetString(UserSessionKey);
            return value == null ? this.FirstTime() : JsonConvert.DeserializeObject<List<User>>(value);
        }

        public User Create(User user)
        {
            var value = _httpContextAccessor.HttpContext.Session.GetString(UserSessionKey);

            List<User> users = value == null ? this.FirstTime() : JsonConvert.DeserializeObject<List<User>>(value);

            if(users.Find(x => x.Id == user.Id) != null)
                users.RemoveAt(users.FindIndex(x => x.Id == user.Id));
            else
            {
                user.Token = _tokenService.GenerateToken(user);
                do
                {
                    int newId = new Random().Next(1, 5000);
                    var existsUser = GetUser().Where(x => x.Id == newId);
                    if (existsUser.Count() < 1)
                        user.Id = newId;
                } while (user.Id == 0);             
            }
            users.Add(user);
            var userJson = JsonConvert.SerializeObject(users);

            _httpContextAccessor.HttpContext.Session.SetString(UserSessionKey, userJson);

            return this.GetUser().Where(x => x.NomeUser.ToUpper() == user.NomeUser.ToUpper() && x.DocumentUser == user.DocumentUser).FirstOrDefault();
        }

        public void Delete(User user)
        {
            var value = _httpContextAccessor.HttpContext.Session.GetString(UserSessionKey);

            List<User> users = value == null ? this.FirstTime() : JsonConvert.DeserializeObject<List<User>>(value);

            users.RemoveAt(users.FindIndex(x => x.Id == user.Id));

            var userJson = JsonConvert.SerializeObject(users);

            _httpContextAccessor.HttpContext.Session.SetString(UserSessionKey, userJson);
        }

    }
    public interface IUserExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<User> FirstTime();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<User> GetUser();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        User Create(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        void Delete(User user);

    }

}
