using BancaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Interfaces.Repository
{
    public interface IBankAccountRepository
    {
        Task AddAccountAsync(BankAccountEntity account);
        Task<bool> ExistsByAccountNumberAsync(string accountNumber);
        Task<BankAccountEntity?> GetByAccountNumberAsync(string accountNumber);
        Task UpdateAccountAsync(BankAccountEntity account);
        Task<BankAccountEntity?> GetHistoryAsync(string accountNumber);
        Task<List<BankAccountEntity>> GetAllAccountsAsync();
    }
}
