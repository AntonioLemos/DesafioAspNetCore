using BlazorApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BlazorApp.Data
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        protected readonly HttpClient _http;
        protected ILocalStorageService _storage;

        public UserService(IHttpClientFactory clientFactory, ILocalStorageService localStorageService, ILocalStorageService storage)
        {
            _clientFactory = clientFactory;
            _storage = storage;
            _http = _clientFactory.CreateClient("ServerAPI");
        }

        public async Task<dynamic> Login(User user)
        {
            var content = await HelpPost("v1/account/login", user);
            var result = JsonConvert.DeserializeObject<dynamic>(content);
            var userResult = JsonConvert.DeserializeObject<User>(result["user"].ToString());
            await _storage.SetItem("USER", userResult);
            return result;
        }
        public async Task<dynamic> GetUserLogon()
        {
            return await _storage.GetItem("USER");
        }

        public async Task<dynamic> GetUsers(User user)
        {
            var content = await HelpPost("v1/account/users", user);      
            return JsonConvert.DeserializeObject<dynamic>(content);
        }
        public async Task<dynamic> Create(User user)
        {
            var content = await HelpPost("v1/account/create", user);
            var result = JsonConvert.DeserializeObject<dynamic>(content);
            var userResult = JsonConvert.DeserializeObject<User>(result["user"].ToString());
            if(!string.IsNullOrEmpty(userResult.Token))
                await _storage.SetItem("USER", userResult);

            return result;
        }

        public async Task<dynamic> Edit(User user)
        {
            var content = await HelpPost("v1/account/edit", user);
            var result = JsonConvert.DeserializeObject<dynamic>(content);
            var userResult = JsonConvert.DeserializeObject<User>(result["user"].ToString());
            if (!string.IsNullOrEmpty(userResult.Token))
                await _storage.SetItem("USER", userResult);

            return result;
        }

        public async Task<dynamic> Delete(int idUser)
        {
            User user = new User();
            user.Id = idUser;
            var content = await HelpPost("v1/account/delete", user);
            return JsonConvert.DeserializeObject<dynamic>(content);
        }

        private async Task<string> HelpPost(string url, object obj)
        {
            var userToken = await _storage.GetItem("USER");
            
            if(userToken != null)
                this._http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: userToken.Token);

            var response = await this._http.PostAsync(requestUri: url,
                new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }
    }
}
