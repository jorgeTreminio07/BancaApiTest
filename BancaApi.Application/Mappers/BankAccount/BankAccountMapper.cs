using AutoMapper;
using BancaApi.Application.Commands.BankAccount;
using BancaApi.Application.DTOS.Response.BankAccount;
using BancaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Mappers.BankAccount
{
    public class BankAccountMapper : Profile
    {
        public BankAccountMapper()
        {
            CreateMap<CreateBankAccountCommand, BankAccountEntity>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialBalance));
            CreateMap<BankAccountEntity, BankAccountResDto>()
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client));
        }
    }
}
