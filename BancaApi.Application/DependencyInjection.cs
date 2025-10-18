using BancaApi.Application.Services;
using BancaApi.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(op => op.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(assembly);

            services.AddScoped<IAccountNumberGenerator, AccountNumberGenerator>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IInterestService, InterestService>();

            return services;
        }
    }
}
