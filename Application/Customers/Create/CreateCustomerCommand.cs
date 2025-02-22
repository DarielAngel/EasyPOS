using MediatR;

namespace Application.Customers.Create;

// Unit es como un void [no retorna nada]
public record CreateCustomerCommand(
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode
) : IRequest<Unit>;