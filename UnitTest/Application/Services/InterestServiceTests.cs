using Xunit;
using BancaApi.Application.Services;
using BancaApi.Domain.Entities;
using System;

namespace UnitTest.Application.Services
{
    public class InterestServiceTests
    {
        private readonly InterestService _service;

        public InterestServiceTests()
        {
            _service = new InterestService();
        }

        [Fact(DisplayName = "Correctly apply interest to the account")]
        public void ApplyInterest_ShouldIncreaseBalance()
        {
            var account = new BankAccountEntity(Guid.NewGuid(), 1000m, "ACC001");
            decimal annualRate = 5.0m;
            decimal newBalance = _service.ApplyInterest(account, annualRate);

            Assert.Equal(1050m, newBalance);
            Assert.Equal(1050m, account.Balance);
        }

        [Fact(DisplayName = "Throws an exception if the interest rate is negative")]
        public void ApplyInterest_NegativeRate_ShouldThrow()
        {
            var account = new BankAccountEntity(Guid.NewGuid(), 1000m, "ACC003");
            decimal annualRate = -5.0m;

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _service.ApplyInterest(account, annualRate));
            Assert.Contains("Annual interest rate cannot be negative", ex.Message);
        }
    }
}
