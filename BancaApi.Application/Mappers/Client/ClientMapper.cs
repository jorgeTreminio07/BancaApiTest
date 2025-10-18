using AutoMapper;
using BancaApi.Application.Commands.Client;
using BancaApi.Application.DTOS.Request.Client;
using BancaApi.Application.DTOS.Response.Client;
using BancaApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Mappers.Client
{
    public class ClientMapper : Profile
    {
        public ClientMapper()
        {
            CreateMap<CreateClientCommand, ClientEntity>();
            CreateMap<ClientEntity, ClientResDto>();
        }
    }
}
