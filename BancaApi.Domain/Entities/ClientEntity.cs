using BancaApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Entities
{
    [Table("Client")]
    public class ClientEntity
    {
        public ClientEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required DateTime Birthday { get; set; }
        public SexType Sex { get; set; }
        public decimal? Income { get; set; }

        public ICollection<BankAccountEntity> BankAccounts { get; set; } = new List<BankAccountEntity>();
    }
}
