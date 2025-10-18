using BancaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Interfaces.Services
{
    public interface IInterestService
    {
        decimal ApplyInterest(BankAccountEntity account, decimal annualRate); 
    }
}
