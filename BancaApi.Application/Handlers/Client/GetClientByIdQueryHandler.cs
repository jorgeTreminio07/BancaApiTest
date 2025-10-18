using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.DTOS.Response.Client;
using BancaApi.Application.Queries.Client;
using BancaApi.Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Handlers.Client
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Result<ClientResDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<Result<ClientResDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.id);

            if (client == null)
            {
                return Result.NotFound();
            } 

            var clientResDto = _mapper.Map<ClientResDto>(client);
            return Result.Success(clientResDto);
        }
    }
}
