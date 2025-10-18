using Ardalis.Result;
using BancaApi.Application.DTOS.Response.Transaction;
using BancaApi.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Commands.Transaction
{
    public record CreateTransactionCommand(
        string AccountNumber,
        TransactionType Type,
        decimal Amount
    ) : IRequest<Result<TransactionResDto>>;
}
