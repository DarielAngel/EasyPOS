using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;
using ErrorOr;

using Domain.DomainErrors;

namespace Application.Customers.Create;

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    
    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if(PhoneNumber.Create(request.PhoneNumber) is not PhoneNumber phoneNumber) {
            //throw new ArgumentException(nameof(phoneNumber));
            return Errors.Customer.PhoneNumberWithBadFormat;
        }

        if(Address.Create(request.Country, request.Line1, request.Line2, 
            request.City, request.State, request.ZipCode) is not Address address) {
            //throw new ArgumentException(nameof(address));
            return Errors.Customer.AddressWithBadFormat;
        }

        var customer = new Customer(
            new CustomerId(Guid.NewGuid()),
            request.Name,
            request.LastName,
            request.Email,
            phoneNumber,
            address,
            true
        );

        await _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
        
    }
}