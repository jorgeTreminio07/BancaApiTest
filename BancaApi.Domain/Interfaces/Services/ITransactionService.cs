using BancaApi.Domain.Entities;
using BancaApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Interfaces.Services
{
    public interface ITransactionService
    {
        decimal ApplyTransaction(BankAccountEntity account, TransactionType type, decimal amount);
    }
}
