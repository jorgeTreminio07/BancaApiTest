using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.DTOS.Response.BankAccount;
using BancaApi.Application.DTOS.Response.Transaction;
using BancaApi.Application.Queries.BankAccount;
using BancaApi.Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Handlers.BankAccount
{
    public class GetBankAccountHistoryQueryHandler : IRequestHandler<GetBankAccountHistoryQuery, Result<HistoryTransactionResDto>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetBankAccountHistoryQueryHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<Result<HistoryTransactionResDto>> Handle(GetBankAccountHistoryQuery request, CancellationToken cancellationToken)
        {
            var account = await _bankAccountRepository.GetHistoryAsync(request.AccountNumber);

            if (account == null)
            {
                return Result.NotFound("Bank account not found.");
            }

            var transactionsDto = account.Transactions
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => _mapper.Map<TransactionResDto>(t))
                .ToList();

            var historyDto = new HistoryTransactionResDto
            {
                AccountNumber = account.AccountNumber,
                ClientName = account.Client.Name,
                Balance = account.Balance,
                Transactions = transactionsDto
            };

            return Result.Success(historyDto);
        }
    }
}
