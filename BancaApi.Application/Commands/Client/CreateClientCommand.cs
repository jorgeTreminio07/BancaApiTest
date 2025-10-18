using Ardalis.Result;
using BancaApi.Application.DTOS.Response.Client;
using BancaApi.Domain.Entities;
using BancaApi.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Commands.Client
{
    public record CreateClientCommand(
        string Name,
        DateTime Birthday,
        SexType Sex,
        decimal Income
    ) : IRequest<Result<ClientResDto>> { }
    
}
