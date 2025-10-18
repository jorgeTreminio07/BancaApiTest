using Ardalis.Result;
using BancaApi.Application.DTOS.Response.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Queries.Client
{
    public record GetClientByIdQuery(Guid id) : IRequest<Result<ClientResDto>>
    {
    }
}
