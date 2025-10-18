using Ardalis.Result;
using AutoMapper;
using BancaApi.Application.Commands.Client;
using BancaApi.Application.DTOS.Response.Client;
using BancaApi.Domain.Entities;
using BancaApi.Domain.Enums;
using BancaApi.Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaApi.Application.Handlers.Client
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Result<ClientResDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<Result<ClientResDto>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(SexType), request.Sex))
            {
                return Result.Error("SexType must be either Male or Female.");
            }

            var clientExist = await _clientRepository.ExistsByNameAsync(request.Name);
            if (clientExist)
            {
                return Result.Error("A client with the same name already exists.");
            }


            var client = _mapper.Map<ClientEntity>(request);
            await _clientRepository.AddClientAsync(client);

            var clientResDto = _mapper.Map<ClientResDto>(client);
            return Result.Success(clientResDto);
        }
    }
}
