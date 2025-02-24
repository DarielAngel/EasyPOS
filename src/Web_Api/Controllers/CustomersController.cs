using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Customers.Create;

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
}