using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;

namespace Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if(PhoneNumber.Create(request.PhoneNumber) is not PhoneNumber phoneNumber) {
            throw new ArgumentException(nameof(phoneNumber));
        }

        if(Address.Create(request.Country, request.Line1, request.Line2, 
            request.City, request.State, request.ZipCode) is not Address address) {
            throw new ArgumentException(nameof(address));
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