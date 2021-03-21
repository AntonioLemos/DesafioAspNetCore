using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApiExample.Models;
using MyApiExample.Repositories;

namespace MyApiExample.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Authenticate(User model)
        {
            User user = null;
            if (!string.IsNullOrEmpty(model.NomeUser) && !string.IsNullOrEmpty(model.DocumentUser))
            {
                user = _userRepo.Login(model.NomeUser, model.DocumentUser);
                if (user == null)
                    return new { user, message = "Nome ou Documento Incorretos!" };

                return new { user, message = "Ok" };
            }
            else
            {
                return new { user, message = "Nome ou Documento Incorretos!" };
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public dynamic GetUsers(User user)
        {
            var users = _userRepo.GetUsers(user);
            return new { users, message = "Ok" };
        }

        public dynamic CreateEditUser(User user)
        {
            if (user.Id == 0)
            {
                var userExist = _userRepo.GetUserbyDocument(user.DocumentUser);
                if (string.IsNullOrEmpty(user.DocumentUser) || !CheckCpf(user.DocumentUser) || userExist != null)
                    return new { user, message = "Documento Inválido" };
            }
                

            if (string.IsNullOrEmpty(user.NomeUser) || user.NomeUser.Length > 70)
                return new { user, message = "Nome Inválido" };

            if (string.IsNullOrEmpty(user.Uf))
                return new { user, message = "Nome Inválido" };

            user.DataNascimento = new DateTime(user.DataNascimento.Year, user.DataNascimento.Month, user.DataNascimento.Day);
            user = _userRepo.CreateEditUser(user);
            return new { user, message = "Ok" };
        }

        public dynamic DeleteUsers(User user)
        {
            _userRepo.DeleteUsers(user);
            user = new User();
            return new { user, message = "Ok" };
        }

        public static bool CheckCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }

}