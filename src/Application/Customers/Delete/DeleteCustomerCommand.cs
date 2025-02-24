using ErrorOr;
using MediatR;

namespace Application.Customers.Delete;

// Unit es como un void [no retorna nada]
public record DeleteCustomerCommand(Guid Id) : IRequest<ErrorOr<Unit>>;