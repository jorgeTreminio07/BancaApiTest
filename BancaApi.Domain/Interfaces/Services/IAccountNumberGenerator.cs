using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Domain.Interfaces.Services
{
    public interface IAccountNumberGenerator
    {
        string Generate();
    }
}
