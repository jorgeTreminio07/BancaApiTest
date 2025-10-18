using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.Commands.Transaction;
using BancaApi.Application.DTOS.Response.Transaction;
using BancaApi.Domain.Entities;
using BancaApi.Domain.Enums;
using BancaApi.Domain.Interfaces.Repository;
using BancaApi.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Handlers.Transaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result<TransactionResDto>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public CreateTransactionCommandHandler(
            IBankAccountRepository bankAccountRepository,
            ITransactionRepository transactionRepository,
            IMapper mapper, ITransactionService transactionService)
        {
            _bankAccountRepository = bankAccountRepository;
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<Result<TransactionResDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAsync(request.AccountNumber);
            if (account == null)
            {
                return Result.NotFound("Account not found.");
            }

            try
            {
                var newBalance = _transactionService.ApplyTransaction(account, request.Type, request.Amount);

                var transaction = new TransactionsEntity(
                    account.Id,
                    request.Type,
                    request.Amount
                );
                transaction.BalanceAfterTransaction = account.Balance;

                await _transactionRepository.AddTransactionAsync(transaction);
                await _bankAccountRepository.UpdateAccountAsync(account);

                var resDto = _mapper.Map<TransactionResDto>(transaction);
                resDto.AccountNumber = account.AccountNumber;

                return Result.Success(resDto);
            }
            catch(InvalidOperationException ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
