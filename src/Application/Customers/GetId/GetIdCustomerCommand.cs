using Application.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetId;

// Unit es como un void [no retorna nada]
public record GetIdCustomerCommand(Guid Id) : IRequest<ErrorOr<CustomerResponse>>;