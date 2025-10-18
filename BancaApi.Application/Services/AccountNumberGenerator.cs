using BancaApi.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Services
{
    public class AccountNumberGenerator : IAccountNumberGenerator
    {
        public string Generate()
        {
            Random random = Random.Shared;

            const int min = 100000000;
            const int max = 1000000000;

            int numero = random.Next(min, max);

            return numero.ToString();
        }
    }
}
