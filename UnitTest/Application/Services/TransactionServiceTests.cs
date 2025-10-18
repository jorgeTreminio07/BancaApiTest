using BancaApi.Application.Services;
using BancaApi.Domain.Entities;
using BancaApi.Domain.Enums;
using BancaApi.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Application.Services
{
    public class TransactionServiceTests
    {
        private readonly ITransactionService _service;
        private readonly Guid _testAccountId = Guid.NewGuid();
        private const string TestAccountNumber = "987654321";

        public TransactionServiceTests()
        {
            _service = new TransactionService();
        }

        private BankAccountEntity CreateTestAccount(decimal initialBalance)
        {
            return new BankAccountEntity(_testAccountId, initialBalance, TestAccountNumber);
        }

        [Fact]
        public void ApplyTransaction_Deposit_IncreasesBalance()
        {
            const decimal initialBalance = 100.00m;
            const decimal depositAmount = 50.00m;
            var account = CreateTestAccount(initialBalance);
            decimal newBalance = _service.ApplyTransaction(account, TransactionType.Deposit, depositAmount);


            Assert.Equal(150.00m, newBalance);
            Assert.Equal(150.00m, account.Balance);
        }

        [Fact]
        public void ApplyTransaction_SuccessfulWithdrawal_DecreasesBalance()
        {
            const decimal initialBalance = 200.00m;
            const decimal withdrawalAmount = 75.00m;
            var account = CreateTestAccount(initialBalance);
            decimal newBalance = _service.ApplyTransaction(account, TransactionType.Withdrawal, withdrawalAmount);

            Assert.Equal(125.00m, newBalance);
            Assert.Equal(125.00m, account.Balance);
        }

        [Fact]
        public void ApplyTransaction_InsufficientBalanceWithdrawal_ThrowsInvalidOperationException()
        {
            const decimal currentBalance = 50.00m;
            const decimal withdrawalAmount = 100.00m;
            var account = CreateTestAccount(currentBalance);

            var exception = Assert.Throws<InvalidOperationException>(
                () => _service.ApplyTransaction(account, TransactionType.Withdrawal, withdrawalAmount)
            );

            Assert.Contains("Insufficient balance for withdrawal.", exception.Message);
            Assert.Equal(currentBalance, account.Balance);
        }

        [Fact]
        public void ApplyTransaction_InvalidType_ThrowsArgumentOutOfRangeException()
        {
            var account = CreateTestAccount(100m);
            const decimal amount = 10.00m;

            Assert.Throws<ArgumentOutOfRangeException>(
                () => _service.ApplyTransaction(account, (TransactionType)99, amount)
            );
        }
    }
}
