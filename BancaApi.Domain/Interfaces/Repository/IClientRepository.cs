using BancaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Interfaces.Repository
{
    public interface IClientRepository
    {
        Task<ClientEntity?> GetByIdAsync(Guid id);
        Task<ClientEntity> AddClientAsync(ClientEntity client);
        Task<bool> ExistsByNameAsync(string name);
    }
}
