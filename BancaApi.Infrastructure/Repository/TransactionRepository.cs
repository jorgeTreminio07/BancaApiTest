using BancaApi.Domain.Entities;
using BancaApi.Domain.Interfaces.Repository;
using BancaApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionsEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions
                                 .Include(t => t.Account)
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTransactionAsync(TransactionsEntity transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
