using BancaApi.Domain.Entities;
using BancaApi.Domain.Enums;
using BancaApi.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Services
{
    public class TransactionService : ITransactionService
    {
        public decimal ApplyTransaction(BankAccountEntity account, TransactionType type, decimal amount)
        {
            if (type == TransactionType.Withdrawal && account.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient balance for withdrawal.");
            }

            switch (type)
            {
                case TransactionType.Deposit:
                    account.Balance += amount;
                    break;
                case TransactionType.Withdrawal:
                    account.Balance -= amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), "Invalid transaction type.");
            }

            return account.Balance;
        }
    }
}
