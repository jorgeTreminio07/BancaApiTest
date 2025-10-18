using Ardalis.Result;
using BancaApi.Application.Commands.Client;
using BancaApi.Application.DTOS.Request.Client;
using BancaApi.Application.DTOS.Response.Client;
using BancaApi.Application.Queries.Client;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancaApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientReqDto clientReq)
        {
            var command = new CreateClientCommand
            (
                clientReq.Name,
                clientReq.Birthday,
                clientReq.Sex,
                clientReq.Income
            );

            var result = await _mediator.Send(command);

            if (result.Status == ResultStatus.Invalid)
                return BadRequest(result.Errors);

            if (result.Status == ResultStatus.Error)
                return StatusCode(500, result.Errors); 

            return CreatedAtAction(nameof(GetClientById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var query = new GetClientByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result);
            }

            return Ok(result.Value);
        }
    }
}
