using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Entities
{
    [Table("BankAccount")]
    public class BankAccountEntity
    {
        protected BankAccountEntity() { }
        public BankAccountEntity(Guid clientId, decimal initialBalance, string accountNumber)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            Balance = initialBalance;
            AccountNumber = accountNumber;
        }

        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
        public ClientEntity Client { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;
        public decimal Balance { get; set; }

        public ICollection<TransactionsEntity> Transactions { get; set; } = new List<TransactionsEntity>();
    }
}
