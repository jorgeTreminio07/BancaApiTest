using AutoMapper;
using BancaApi.Application.Commands.Transaction;
using BancaApi.Application.DTOS.Response.Transaction;
using BancaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Mappers.Transaction
{
    public class TransactionMapper : Profile
    {
        public TransactionMapper()
        {
            CreateMap<CreateTransactionCommand, TransactionsEntity>();
            CreateMap<TransactionsEntity, TransactionResDto>();
        }
    }
}
