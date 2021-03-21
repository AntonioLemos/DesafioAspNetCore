using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Models
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
}
