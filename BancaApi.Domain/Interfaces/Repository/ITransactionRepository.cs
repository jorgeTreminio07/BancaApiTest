using BancaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        Task<TransactionsEntity?> GetByIdAsync(Guid id);
        Task AddTransactionAsync(TransactionsEntity transaction);
    }
}
