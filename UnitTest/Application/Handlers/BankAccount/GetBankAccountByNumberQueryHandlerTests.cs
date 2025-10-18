using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.DTOS.Response.BankAccount;
using BancaApi.Application.Handlers.BankAccount;
using BancaApi.Application.Queries.BankAccount;
using BancaApi.Domain.Entities;
using BancaApi.Domain.Interfaces.Repository;
using Moq;
using Xunit;
using System;

namespace UnitTest.Application.Handlers.BankAccount
{
    public class GetBankAccountByNumberQueryHandlerTests
    {
        private readonly Mock<IBankAccountRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetBankAccountByNumberQueryHandler _handler;

        public GetBankAccountByNumberQueryHandlerTests()
        {
            _mockRepository = new Mock<IBankAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetBankAccountByNumberQueryHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_AccountFound_ReturnsSuccessWithDto()
        {
            var accountNumber = "123456";
            var initialBalance = 500.00m;
            var clientId = Guid.NewGuid(); 

            var query = new GetBankAccountByNumberQuery(accountNumber);
            var bankAccountEntity = new BankAccountEntity(clientId, initialBalance, accountNumber);

            var expectedDto = new BankAccountResDto { AccountNumber = accountNumber, Balance = initialBalance };
            _mockRepository
                .Setup(r => r.GetByAccountNumberAsync(accountNumber))
                .ReturnsAsync(bankAccountEntity);

            _mockMapper
                .Setup(m => m.Map<BankAccountResDto>(bankAccountEntity))
                .Returns(expectedDto);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(ResultStatus.Ok, result.Status);

            Assert.Equal(expectedDto, result.Value);
            Assert.Equal(expectedDto.AccountNumber, result.Value.AccountNumber);

            _mockRepository.Verify(r => r.GetByAccountNumberAsync(accountNumber), Times.Once);
            _mockMapper.Verify(m => m.Map<BankAccountResDto>(bankAccountEntity), Times.Once);
        }

        [Fact]
        public async Task Handle_AccountNotFound_ReturnsNotFoundResult()
        {
            var accountNumber = "999999";
            var query = new GetBankAccountByNumberQuery(accountNumber);

            _mockRepository
                .Setup(r => r.GetByAccountNumberAsync(accountNumber))
                .ReturnsAsync((BankAccountEntity?)null);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(ResultStatus.NotFound, result.Status);

            _mockRepository.Verify(r => r.GetByAccountNumberAsync(accountNumber), Times.Once);

            _mockMapper.Verify(m => m.Map<BankAccountResDto>(It.IsAny<BankAccountEntity>()), Times.Never);
        }
    }
}