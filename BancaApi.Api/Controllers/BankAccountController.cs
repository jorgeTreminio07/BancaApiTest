using Ardalis.Result;
using BancaApi.Application.Commands.BankAccount;
using BancaApi.Application.DTOS.Request.BankAccount;
using BancaApi.Application.Queries.BankAccount;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancaApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetByAccountNumber(string accountNumber)
        {
            var query = new GetBankAccountByNumberQuery(accountNumber);
            var result = await _mediator.Send(query);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            if (result.Status == ResultStatus.Invalid)
                return BadRequest(result.Errors);

            if (result.Status == ResultStatus.Error)
                return StatusCode(500, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] BankAccountReqDto req)
        {
            var command = new CreateBankAccountCommand(req.ClientId, req.InitialBalance);
            var result = await _mediator.Send(command);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            if (result.Status == ResultStatus.Invalid)
                return BadRequest(result.Errors);

            if (result.Status == ResultStatus.Error)
                return StatusCode(500, result.Errors);

            return CreatedAtAction(nameof(GetByAccountNumber), new { accountNumber = result.Value.AccountNumber }, result.Value);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetAccountHistory([FromQuery] string accountNumber)
        {
            var query = new GetBankAccountHistoryQuery(accountNumber);
            var result = await _mediator.Send(query);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            if (result.Status == ResultStatus.Invalid)
                return BadRequest(result.Errors);

            if (result.Status == ResultStatus.Error)
                return StatusCode(500, result.Errors);

            return Ok(result.Value);
        }

    }
}

