using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.DTOS.Response.Transaction;
using BancaApi.Application.Queries.Transaction;
using BancaApi.Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Handlers.Transaction
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, Result<TransactionResDto>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<Result<TransactionResDto>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.Id);

            if (transaction == null)
                return Result.NotFound("Transaction not found.");

            var resDto = _mapper.Map<TransactionResDto>(transaction);
            resDto.AccountNumber = transaction.Account.AccountNumber;
            return Result.Success(resDto);
        }
    }
}
