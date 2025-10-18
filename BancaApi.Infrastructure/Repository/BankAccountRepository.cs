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
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly AppDbContext _context;

        public BankAccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<BankAccountEntity?> GetByAccountNumberAsync(string accountNumber)
        {
            return await _context.BankAccounts
                         .Include(a => a.Client)
                         .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
        }
        public async Task<bool> ExistsByAccountNumberAsync(string accountNumber)
        {
            return await _context.BankAccounts.AnyAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task AddAccountAsync(BankAccountEntity account)
        {
            _context.BankAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(BankAccountEntity account)
        {
            _context.BankAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<BankAccountEntity?> GetHistoryAsync(string accountNumber)
        {
            return await _context.BankAccounts
                .Include(a => a.Client)
                .Include(a => a.Transactions)
                .Where(a => a.AccountNumber == accountNumber)
                .OrderByDescending(a => a.Transactions.OrderByDescending(t => t.CreatedAt).Select(t => t.CreatedAt).FirstOrDefault())
                .FirstOrDefaultAsync();
        }

        public async Task<List<BankAccountEntity>> GetAllAccountsAsync()
        {
            return await _context.BankAccounts
                                 .Include(a => a.Client)
                                 .ToListAsync();
        }
    }
}
