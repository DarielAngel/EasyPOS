using Domain.Customers;
using MediatR;
using ErrorOr;

using Domain.DomainErrors;
using Application.Common;

namespace Application.Customers.GetId;

public sealed class GetIdCustomerCommandHandler : IRequestHandler<GetIdCustomerCommand, ErrorOr<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetIdCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }
    
    public async Task<ErrorOr<CustomerResponse>> Handle(GetIdCustomerCommand command, CancellationToken cancellationToken)
    {
        if(await _customerRepository.GetByIdAsync(new CustomerId(command.Id)) is not Customer customer) {
            return Errors.Customer.CustomerByIdNotFound;
        }

        return new CustomerResponse(
            customer.Id.Value,
            customer.Name,
            customer.Email,
            customer.PhoneNumber.Value,
            new AddressResponse(
                customer.Address.Country,
                customer.Address.Line1,
                customer.Address.Line2,
                customer.Address.City,
                customer.Address.State,
                customer.Address.ZipCode
            ),
            customer.Active
        );
        
    }
}