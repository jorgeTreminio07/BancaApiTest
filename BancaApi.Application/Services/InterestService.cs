using BancaApi.Domain.Entities;
using BancaApi.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Services
{
    public class InterestService : IInterestService
    {
        public decimal ApplyInterest(BankAccountEntity account, decimal annualRate)
        {
            // 1. Validación de la tasa de interés
            if (annualRate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(annualRate), "Annual interest rate cannot be negative.");
            }

            // 2. Cálculo del monto de interés
            // Dividimos entre 100 para convertir el porcentaje (ej. 5.0) en decimal (0.05)
            decimal interestFactor = annualRate / 100.0m;
            decimal interestAmount = account.Balance * interestFactor;

            // 3. Aplicación del interés al saldo
            // Usamos decimal.Round para asegurar precisión monetaria (dos decimales)
            interestAmount = decimal.Round(interestAmount, 2);
            account.Balance += interestAmount;

            return account.Balance;
        }
    }
}
