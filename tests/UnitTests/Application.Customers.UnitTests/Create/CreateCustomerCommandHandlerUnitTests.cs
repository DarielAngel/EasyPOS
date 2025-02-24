using Domain.Customers;
using Domain.Primitives;
using Application.Customers.Create;
using Domain.DomainErrors;

namespace Application.Customers.UnitTests.Application.Customers.Unitests.Create;

public class CreateCustomerCommandHandlerUnitTests
{
    

    /*
        NOMENCLATURA A SEGUIR:
        - ¿Qué vamos a probar?
        - El escenario
        - Lo que vamos a arrojar
    */

    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerUnitTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new CreateCustomerCommandHandler(_mockCustomerRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {
        // Arrange (se configura los parametros de entrada de nuestra prueba unitaria)
        CreateCustomerCommand command = new CreateCustomerCommand(
            "Dariel",
            "yyy",
            "a@gmail.com",
            "573495354",
            "CUB",
            "",
            "",
            "Hundf",
            "hshshs",
            "5489"
        );
        
        // Act (se ejecuta el método a probar de nuestra prueba unitaria)
        var result = await _handler.Handle(command, default);
        
        // Assert (se verifica los datos de retorno de nuestro método probado en la prueba unitaria)
        result.IsError.Should().BeTrue(); // hay error
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Description);
    }

    [Fact]
    public async Task HandleCreateCustomer_WhenAddressHasBadFormat_ShouldReturnValidationError()
    {
        // Arrange (se configura los parametros de entrada de nuestra prueba unitaria)
        CreateCustomerCommand command = new CreateCustomerCommand(
            "Dariel",
            "yyy",
            "a@gmail.com",
            "5734-5354",
            "CUB",
            "Adress Line 1",
            "Adress Line 2",
            "",
            "hshshs",
            "5489"
        );
        
        // Act (se ejecuta el método a probar de nuestra prueba unitaria)
        var result = await _handler.Handle(command, default);
        
        // Assert (se verifica los datos de retorno de nuestro método probado en la prueba unitaria)
        result.IsError.Should().BeTrue(); // hay error
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Customer.AddressWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Customer.AddressWithBadFormat.Description);
    }
}