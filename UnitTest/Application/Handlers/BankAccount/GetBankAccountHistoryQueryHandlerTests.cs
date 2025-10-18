using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.DTOS.Response.Transaction;
using BancaApi.Application.Handlers.BankAccount;
using BancaApi.Application.Queries.BankAccount;
using BancaApi.Domain.Entities;
using BancaApi.Domain.Enums;
using BancaApi.Domain.Interfaces.Repository;
using Moq;
using BancaApi.Application.DTOS.Response.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Application.Handlers.BankAccount
{
    public class GetBankAccountHistoryQueryHandlerTests
    {
        private readonly Mock<IBankAccountRepository> _mockAccountRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetBankAccountHistoryQueryHandler _handler;

        private const string TestAccountNumber = "987654321";
        private readonly Guid TestClientId = Guid.NewGuid();
        private const string TestClientName = "Jorge Treminio";

        public GetBankAccountHistoryQueryHandlerTests()
        {
            _mockAccountRepository = new Mock<IBankAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetBankAccountHistoryQueryHandler(_mockAccountRepository.Object, _mockMapper.Object);
        }

        private BankAccountEntity CreateMockAccount(decimal balance, List<TransactionsEntity> transactions)
        {
            var client = new ClientEntity { Id = TestClientId, Name = TestClientName, Birthday = DateTime.Now, Sex = SexType.Male };

            var account = new BankAccountEntity(TestClientId, balance, TestAccountNumber)
            {
                Balance = balance,
                Client = client,
                Transactions = transactions
            };
            return account;
        }


        [Fact]
        public async Task Handle_AccountFoundWithTransactions_ReturnsSuccessWithHistory()
        {
            const decimal finalBalance = 150.00m;

            var transactionEntities = new List<TransactionsEntity>
            {
                new TransactionsEntity(TestClientId, TransactionType.Deposit, 100m) { BalanceAfterTransaction = 150m, CreatedAt = DateTime.UtcNow.AddMinutes(-5) },
                new TransactionsEntity(TestClientId, TransactionType.Deposit, 50m) { BalanceAfterTransaction = 50m, CreatedAt = DateTime.UtcNow.AddMinutes(-10) }
            };

            var accountEntity = CreateMockAccount(finalBalance, transactionEntities);

            _mockAccountRepository
                .Setup(r => r.GetHistoryAsync(TestAccountNumber))
                .ReturnsAsync(accountEntity);

            var transactionDtos = new List<TransactionResDto>
            {
                new TransactionResDto { Amount = 100m, Type = TransactionType.Deposit },
                new TransactionResDto { Amount = 50m, Type = TransactionType.Deposit }
            };
            _mockMapper.Setup(m => m.Map<TransactionResDto>(It.IsAny<TransactionsEntity>()))
                       .Returns(new Queue<TransactionResDto>(transactionDtos).Dequeue);


            var query = new GetBankAccountHistoryQuery(TestAccountNumber);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            Assert.Equal(finalBalance, result.Value!.Balance);
            Assert.Equal(TestClientName, result.Value.ClientName);
            Assert.Equal(TestAccountNumber, result.Value.AccountNumber);
            Assert.Equal(2, result.Value.Transactions!.Count); 

            Assert.Equal(100m, result.Value.Transactions.First().Amount);

            _mockAccountRepository.Verify(r => r.GetHistoryAsync(TestAccountNumber), Times.Once);
        }

        [Fact]
        public async Task Handle_AccountFoundNoTransactions_ReturnsSuccessWithEmptyHistory()
        {
            const decimal initialBalance = 0.00m;
            var accountEntity = CreateMockAccount(initialBalance, new List<TransactionsEntity>());

            _mockAccountRepository
                .Setup(r => r.GetHistoryAsync(TestAccountNumber))
                .ReturnsAsync(accountEntity);

            _mockMapper.Setup(m => m.Map<TransactionResDto>(It.IsAny<TransactionsEntity>())).Returns(new TransactionResDto());


            var query = new GetBankAccountHistoryQuery(TestAccountNumber);
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            Assert.Equal(initialBalance, result.Value!.Balance);

            Assert.Empty(result.Value.Transactions!);

            _mockAccountRepository.Verify(r => r.GetHistoryAsync(TestAccountNumber), Times.Once);
            _mockMapper.Verify(m => m.Map<TransactionResDto>(It.IsAny<TransactionsEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_AccountNotFound_ReturnsNotFoundResult()
        {
            _mockAccountRepository
                .Setup(r => r.GetHistoryAsync(TestAccountNumber))
                .ReturnsAsync((BankAccountEntity)null!);

            var query = new GetBankAccountHistoryQuery(TestAccountNumber);
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);
            Assert.Contains("Bank account not found.", result.Errors);

            _mockMapper.Verify(m => m.Map<HistoryTransactionResDto>(It.IsAny<BankAccountEntity>()), Times.Never);
            _mockAccountRepository.Verify(r => r.GetHistoryAsync(TestAccountNumber), Times.Once);
        }
    }
}
