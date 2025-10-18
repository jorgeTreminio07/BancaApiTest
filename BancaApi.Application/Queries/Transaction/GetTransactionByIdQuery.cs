using Ardalis.Result;
using BancaApi.Application.DTOS.Response.Transaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Queries.Transaction
{
    public record GetTransactionByIdQuery(Guid Id) : IRequest<Result<TransactionResDto>>;
}
