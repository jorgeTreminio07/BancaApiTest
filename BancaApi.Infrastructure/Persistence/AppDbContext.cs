using BancaApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Infrastructure.Persistence
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientEntity>()
                .HasMany(c => c.BankAccounts)
                .WithOne(b => b.Client)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccountEntity>()
                .HasMany(b => b.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccountEntity>()
                .HasIndex(b => b.AccountNumber)
                .IsUnique();

        }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<BankAccountEntity> BankAccounts { get; set; }
        public DbSet<TransactionsEntity> Transactions { get; set; }
    }
}
