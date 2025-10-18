using Ardalis.Result;
using BancaApi.Application.DTOS.Response.BankAccount;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Queries.BankAccount
{
    public record GetBankAccountHistoryQuery(string AccountNumber) : IRequest<Result<HistoryTransactionResDto>> { }
}
