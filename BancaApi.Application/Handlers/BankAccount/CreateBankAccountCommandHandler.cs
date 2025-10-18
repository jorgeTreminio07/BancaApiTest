using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.Commands.BankAccount;
using BancaApi.Application.DTOS.Response.BankAccount;
using BancaApi.Domain.Entities;
using BancaApi.Domain.Interfaces.Repository;
using BancaApi.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Handlers.BankAccount
{
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Result<BankAccountResDto>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAccountNumberGenerator _accountNumberGenerator;
        private readonly IMapper _mapper;

        public CreateBankAccountCommandHandler(
            IBankAccountRepository bankAccountRepository,
            IAccountNumberGenerator accountNumberGenerator,
            IMapper mapper, IClientRepository clientRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _accountNumberGenerator = accountNumberGenerator;
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<Result<BankAccountResDto>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var clientExist =  await _clientRepository.GetByIdAsync(request.ClientId);
            if (clientExist == null)
            {
                return Result.NotFound("Client not found.");
            }

            var accountNumber = _accountNumberGenerator.Generate();

            var existsAccount = await _bankAccountRepository.ExistsByAccountNumberAsync(accountNumber);
            if (existsAccount)
            {
                return Result.Error("Account number already exists.");
            }

            var banckAccount = _mapper.Map<BankAccountEntity>(request);
            banckAccount.AccountNumber = accountNumber;
            banckAccount.Client = clientExist;

            await _bankAccountRepository.AddAccountAsync(banckAccount);

            var banckAccountDto = _mapper.Map<BankAccountResDto>(banckAccount);

            return Result.Success(banckAccountDto);
        }
    }
}
