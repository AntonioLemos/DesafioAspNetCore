using Microsoft.AspNetCore.Mvc.Testing;
using MyApiExample;
using MyApiExample.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject1
{
    public class IntegrationTest
    {
        protected readonly HttpClient _testclient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _testclient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _testclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: await GetJwtAsync());
        }

        protected async Task<dynamic> GetUser(User user)
        {
            return await _testclient.PostAsync(requestUri: "v1/account/users",
                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
        }


        protected async Task<dynamic> CreateUser(User user)
        {
            user = new User();
            user.NomeUser = "Antonio";
            user.DocumentUser = "12365478998";
            return await _testclient.PostAsync(requestUri: "v1/account/create",
                            new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
        }

        protected async Task<dynamic> EditUser(User user)
        {
            user = new User();
            user.NomeUser = "Antonio";
            user.DocumentUser = "12365478998";
            return await _testclient.PostAsync(requestUri: "v1/account/edit",
                            new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
        }

        protected async Task<dynamic> DeleteUser(User user)
        {
            return await _testclient.PostAsync(requestUri: "v1/account/edit",
                            new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
        }

        private async Task<string> GetJwtAsync()
        {
            var user = new User();
            user.NomeUser = "Antonio";
            user.DocumentUser = "12365478998";
            var response = await _testclient.PostAsync(requestUri: "v1/account/login", 
                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();
            JObject objResult = JsonConvert.DeserializeObject<dynamic>(content);
            var token = (string)objResult["user"]["token"];

            return token.ToString();
        }
    }
}
