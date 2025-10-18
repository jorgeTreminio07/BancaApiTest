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
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClientEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<ClientEntity> AddClientAsync(ClientEntity client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Clients
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
