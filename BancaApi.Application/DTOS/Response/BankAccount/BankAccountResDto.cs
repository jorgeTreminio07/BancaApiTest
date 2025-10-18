using BancaApi.Application.DTOS.Response.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.DTOS.Response.BankAccount
{
    public class BankAccountResDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string AccountNumber { get; set; } = null!;
        public decimal Balance { get; set; }
        public ClientResDto Client { get; set; } = null!;
    }
}
