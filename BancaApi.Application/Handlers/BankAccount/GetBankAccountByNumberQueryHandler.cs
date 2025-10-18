using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.DTOS.Response.BankAccount;
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
    public class GetBankAccountByNumberQueryHandler : IRequestHandler<GetBankAccountByNumberQuery, Result<BankAccountResDto>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetBankAccountByNumberQueryHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<Result<BankAccountResDto>> Handle(GetBankAccountByNumberQuery request, CancellationToken cancellationToken)
        {
            var account = await _bankAccountRepository.GetByAccountNumberAsync(request.AccountNumber);
            if (account == null)
            {
                return Result.NotFound();
            }    

            var resDto = _mapper.Map<BankAccountResDto>(account);
            return Result.Success(resDto);
        }
    }
}
