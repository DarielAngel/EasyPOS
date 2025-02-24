using ErrorOr;
using MediatR;

namespace Application.Customers.Update;

// Unit es como un void [no retorna nada]
public record UpdateCustomerCommand(
    Guid Id,
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode,
    bool Active
) : IRequest<ErrorOr<Unit>>;