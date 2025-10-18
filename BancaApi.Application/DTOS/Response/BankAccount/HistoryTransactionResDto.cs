using BancaApi.Application.DTOS.Response.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.DTOS.Response.BankAccount
{
    public class HistoryTransactionResDto
    {
        public string ClientName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public decimal Balance { get; set; }
        public List<TransactionResDto>? Transactions { get; set; } 
    }
}
