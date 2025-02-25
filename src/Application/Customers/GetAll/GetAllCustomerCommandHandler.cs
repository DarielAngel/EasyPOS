using Application.Common;
using Domain.Customers;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetAll;

public sealed class GetAllCustomerCommandHandler : IRequestHandler<GetAllCustomerCommand, ErrorOr<IReadOnlyList<CustomerResponse>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<CustomerResponse>>> Handle(GetAllCustomerCommand request, CancellationToken cancellationToken)
    {
        
        IReadOnlyList<Customer> lista = await _customerRepository.GetAllAsync();


        return lista.Select(c => new CustomerResponse(
            c.Id.Value,
            c.FullName,
            c.Email,
            c.PhoneNumber.Value,
            new AddressResponse(
                c.Address.Country,
                c.Address.Line1,
                c.Address.Line2,
                c.Address.City,
                c.Address.State,
                c.Address.ZipCode
            ),
            c.Active
        )).ToList();
    }
}