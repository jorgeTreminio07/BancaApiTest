using Ardalis.Result;
using BancaApi.Application.Commands.Transaction;
using BancaApi.Application.DTOS.Request.Transaction;
using BancaApi.Application.Queries.Transaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancaApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionReqDto transactionReq)
        {
            var command = new CreateTransactionCommand(
                transactionReq.AccountNumber,
                transactionReq.Type,
                transactionReq.Amount
            );

            var result = await _mediator.Send(command);

            if (result.Status == ResultStatus.Invalid)
                return BadRequest(result.Errors);

            if (result.Status == ResultStatus.Error)
                return BadRequest(result.Errors);

            if (result.Status == ResultStatus.NotFound)
                return NotFound(result.Errors);

            return CreatedAtAction(nameof(GetTransactionById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var query = new GetTransactionByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.Status == ResultStatus.NotFound)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }
    }
}

