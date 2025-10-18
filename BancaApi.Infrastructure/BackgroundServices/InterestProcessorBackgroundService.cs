using BancaApi.Domain.Entities;
using BancaApi.Domain.Interfaces.Repository;
using BancaApi.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BancaApi.Infrastructure.BackgroundServices
{
    public class InterestProcessorBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InterestProcessorBackgroundService> _logger;
        private readonly decimal _annualInterestRate;
        private readonly int _batchSize;

        public InterestProcessorBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<InterestProcessorBackgroundService> logger,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            _annualInterestRate = configuration.GetValue<decimal>("InterestSettings:AnnualRate", 3.5m);
            _batchSize = configuration.GetValue<int>("InterestSettings:BatchSize", 100);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("InterestProcessorBackgroundService started.");

            await ProcessAllAccountsAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Waiting 24 hours for the next interest processing...");
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);

                await ProcessAllAccountsAsync(stoppingToken);
            }
        }

        public async Task ProcessAllAccountsAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var accountRepo = scope.ServiceProvider.GetRequiredService<IBankAccountRepository>();
                var interestService = scope.ServiceProvider.GetRequiredService<IInterestService>();

                try
                {
                    _logger.LogInformation("Recovering bank accounts to apply interest...");
                    var allAccounts = await accountRepo.GetAllAccountsAsync() ?? new List<BankAccountEntity>();

                    for (int i = 0; i < allAccounts.Count; i += _batchSize)
                    {
                        var batch = allAccounts.GetRange(i, Math.Min(_batchSize, allAccounts.Count - i));

                        foreach (var account in batch)
                        {
                            try
                            {
                                interestService.ApplyInterest(account, _annualInterestRate);
                                await accountRepo.UpdateAccountAsync(account);
                                _logger.LogInformation("Interest applied to account {AccountNumber}. New balance: {Balance}", account.AccountNumber, account.Balance);
                            }
                            catch (Exception innerEx)
                            {
                                _logger.LogError(innerEx, "Error applying interest to account {AccountNumber}", account.AccountNumber);
                            }
                        }
                    }

                    _logger.LogInformation("Interest processing completed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "General error applying interest to accounts.");
                }
            }
        }
    }
}
