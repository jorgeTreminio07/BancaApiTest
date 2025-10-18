using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.Commands.BankAccount;
using BancaApi.Application.DTOS.Response.BankAccount;
using BancaApi.Application.Handlers.BankAccount;
using BancaApi.Domain.Entities;
using BancaApi.Domain.Enums;
using BancaApi.Domain.Interfaces.Repository;
using BancaApi.Domain.Interfaces.Services;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BancaApi.Tests.Application.Handlers.BankAccount
{
    public class CreateBankAccountCommandHandlerTests
    {
        private readonly Mock<IBankAccountRepository> _mockAccountRepository;
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly Mock<IAccountNumberGenerator> _mockNumberGenerator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateBankAccountCommandHandler _handler;

        private readonly Guid _testClientId = Guid.NewGuid();
        private const string TestAccountNumber = "987654321";
        private const decimal TestInitialBalance = 100.00m;

        public CreateBankAccountCommandHandlerTests()
        {
            _mockAccountRepository = new Mock<IBankAccountRepository>();
            _mockClientRepository = new Mock<IClientRepository>();
            _mockNumberGenerator = new Mock<IAccountNumberGenerator>();
            _mockMapper = new Mock<IMapper>();

            _mockNumberGenerator
                .Setup(g => g.Generate())
                .Returns(TestAccountNumber);

            _handler = new CreateBankAccountCommandHandler(
                _mockAccountRepository.Object,
                _mockNumberGenerator.Object,
                _mockMapper.Object,
                _mockClientRepository.Object
            );
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesAccountAndReturnsSuccess()
        {
            var command = new CreateBankAccountCommand(_testClientId, TestInitialBalance);

            var clientEntity = new ClientEntity
            {
                Id = _testClientId,
                Name = "Test Client",
                Birthday = DateTime.Now,
                Sex = SexType.Male
            };

            var accountEntity = new BankAccountEntity(
                _testClientId,
                TestInitialBalance,
                TestAccountNumber
            );

            var expectedDto = new BankAccountResDto
            {
                Id = accountEntity.Id,
                AccountNumber = TestAccountNumber,
                Balance = TestInitialBalance,
                ClientId = _testClientId
            };

            _mockClientRepository
                .Setup(r => r.GetByIdAsync(_testClientId))
                .ReturnsAsync(clientEntity);

           _mockAccountRepository
                .Setup(r => r.ExistsByAccountNumberAsync(TestAccountNumber))
                .ReturnsAsync(false);

            _mockMapper
                .Setup(m => m.Map<BankAccountEntity>(command))
                .Returns(accountEntity);

            _mockMapper
                .Setup(m => m.Map<BankAccountResDto>(accountEntity))
                .Returns(expectedDto);


            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(expectedDto.AccountNumber, result.Value.AccountNumber);
            Assert.Equal(expectedDto.Balance, result.Value.Balance);

            _mockAccountRepository.Verify(
                r => r.AddAccountAsync(
                    It.Is<BankAccountEntity>(
                        a => a.AccountNumber == TestAccountNumber && a.Balance == TestInitialBalance
                    )
                ),
                Times.Once,
                "AddAccountAsync must be called with the correct entity."
            );

            _mockClientRepository.Verify(r => r.GetByIdAsync(_testClientId), Times.Once);
        }
    }
}