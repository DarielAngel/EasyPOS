using Domain.Customers;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;
using Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace Application.Customers.Update;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    private List<Error> error;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        error = new List<Error>();
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {

        if(!await _customerRepository.ExistAsync(new CustomerId(command.Id))) {
            return Errors.Customer.CustomerWithBadFormat;
        }

        if(PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber) {
            return Errors.Customer.CustomerWithBadFormat;
        }

        if(Address.Create(command.Country, command.Line1, command.Line2, command.City, command.State, command.ZipCode) is not Address address) {
            return Errors.Customer.CustomerWithBadFormat;
        }

        Customer customer = Customer.UpdateCustomer(
            command.Id,
            command.Name,
            command.LastName,
            command.Email,
            phoneNumber,
            address,
            command.Active
        );

        await _customerRepository.Update(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}