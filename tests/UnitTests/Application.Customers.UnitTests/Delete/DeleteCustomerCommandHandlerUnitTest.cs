using Domain.Customers;
using Domain.Primitives;
using Application.Customers.Delete;
using Domain.DomainErrors;
using Application.Customers.Create;

namespace Application.Customers.UnitTests.Application.Customers.Unitests.Delete;

public class DeleteCustomerCommandHandlerUnitTests
{
    

    /*
        NOMENCLATURA A SEGUIR:
        - ¿Qué vamos a probar?
        - El escenario
        - Lo que vamos a arrojar
    */

    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly DeleteCustomerCommandHandler _handler;

    public DeleteCustomerCommandHandlerUnitTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new DeleteCustomerCommandHandler(_mockCustomerRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleDeleteCustomer_WhenCustomerIdHasNotFound_ShouldReturnValidationError()
    {
        // Arrange (se configura los parametros de entrada de nuestra prueba unitaria)
        CreateCustomerCommand customer = new CreateCustomerCommand(
            "Dariel",
            "yyy",
            "a@gmail.com",
            "573495354",
            "CUB",
            "Address Line 1",
            "Address Line 2",
            "Hundf",
            "hshshs",
            "5489"
        );

        DeleteCustomerCommand command = new DeleteCustomerCommand(Guid.NewGuid());

        System.Console.WriteLine("Id del cliente de prueba: "+command.Id);
        
        // Act (se ejecuta el método a probar de nuestra prueba unitaria)
        var result = await _handler.Handle(command, default);
        
        // Assert (se verifica los datos de retorno de nuestro método probado en la prueba unitaria)
        result.IsError.Should().BeTrue(); // hay error
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Customer.CustomerByIdNotFound.Code);
        result.FirstError.Description.Should().Be(Errors.Customer.CustomerByIdNotFound.Description);
    }

}