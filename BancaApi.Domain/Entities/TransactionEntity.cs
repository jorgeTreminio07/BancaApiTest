using BancaApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Entities
{
    [Table("Transactions")]
    public class TransactionsEntity
    {
        public TransactionsEntity(Guid accountId, TransactionType type, decimal amount)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Type = type;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid AccountId { get; set; }
        public BankAccountEntity Account { get; set; } = null!;
    }
}

