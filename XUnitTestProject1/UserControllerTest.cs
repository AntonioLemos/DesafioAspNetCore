using MyApiExample.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class UserControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetUsersTest()
        {
            await AuthenticateAsync();
            User user = new User();

            var response = await GetUser(user);

            response.StatusCode = HttpStatusCode.OK;
            await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task GetByUfTest()
        {
            await AuthenticateAsync();

            var user = new User();
            user.Uf = "MG";

            var response = await GetUser(user);

            response.StatusCode = HttpStatusCode.OK;
            await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task GetByIDTest()
        {
            await AuthenticateAsync();

            var user = new User();
            user.Id = 1;

            var response = await GetUser(user);

            response.StatusCode = HttpStatusCode.OK;
            await response.Content.ReadAsStringAsync();
        }
        [Fact]
        public async Task CreateUserTest()
        {
            await AuthenticateAsync();

            var user = new User();
            user.NomeUser = "Teste user";
            user.DocumentUser = "69688744662";
            user.Uf = "SP";
            user.DataNascimento = DateTime.UtcNow;

            var createResponse = await CreateUser(user);

            createResponse.StatusCode = HttpStatusCode.OK;

            await createResponse.Content.ReadAsStringAsync();
        }
        [Fact]
        public async Task EditUserTest()
        {
            await AuthenticateAsync();

            var user = new User();
            user.Id = 1;
            user.NomeUser = "Antonio Test Edit";
            user.DocumentUser = "12365478998";
            user.Uf = "MG";
            user.DataNascimento = DateTime.UtcNow;

            var createResponse = await EditUser(user);

            createResponse.StatusCode = HttpStatusCode.OK;

            await createResponse.Content.ReadAsStringAsync();
        }
        [Fact]
        public async Task DeleteUserTest()
        {
            await AuthenticateAsync();

            var user = new User();
            user.Id = 1;

            var createResponse = await DeleteUser(user);

            createResponse.StatusCode = HttpStatusCode.OK;

            await createResponse.Content.ReadAsStringAsync();
        }
    }
}
