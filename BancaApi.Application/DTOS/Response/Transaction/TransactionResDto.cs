using BancaApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.DTOS.Response.Transaction
{
    public class TransactionResDto
    {
        public Guid Id { get; set; }
        public string? AccountNumber { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
