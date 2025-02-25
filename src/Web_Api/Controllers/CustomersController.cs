using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.GetAll;
using ErrorOr;
using Application.Customers.GetId;
using Application.Customers.Update;

namespace Web_Api.Controllers;

[Route("customers")]
public class Customers : ApiController {
    private readonly ISender _mediator;

    public Customers(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command) {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command) {
        if(command.Id != id) {
            List<Error> error = new () {
                Error.Validation("Customer.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(error);
        }
        
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            customer => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
        var delete = await _mediator.Send(new DeleteCustomerCommand(id));

        return delete.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var result = await _mediator.Send(new GetAllCustomerCommand());

        return result.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) {
        var result = await _mediator.Send(new GetIdCustomerCommand(id));

        return result.Match(
            customer => Ok(),
            errors => Problem(errors)
        );
    }

}