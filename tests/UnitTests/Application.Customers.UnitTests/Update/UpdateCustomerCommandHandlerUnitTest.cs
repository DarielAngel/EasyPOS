using Domain.Customers;
using Domain.Primitives;
using Application.Customers.Update;
using Domain.DomainErrors;

namespace Application.Customers.UnitTests.Application.Customers.Unitests.Update;

public class UpdateCustomerCommandHandlerUnitTests
{
    

    /*
        NOMENCLATURA A SEGUIR:
        - ¿Qué vamos a probar?
        - El escenario
        - Lo que vamos a arrojar
    */

    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly UpdateCustomerCommandHandler _handler;

    public UpdateCustomerCommandHandlerUnitTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new UpdateCustomerCommandHandler(_mockCustomerRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleDeleteCustomer_WhenCustomerIdHasNotFound_ShouldReturnValidationError()
    {
        // Arrange (se configura los parametros de entrada de nuestra prueba unitaria)
        UpdateCustomerCommand command = new UpdateCustomerCommand(
            Guid.NewGuid(),
            "Dariel",
            "yyy",
            "a@gmail.com",
            "573495354",
            "CUB",
            "Address Line 1",
            "Address Line 2",
            "Hundf",
            "hshshs",
            "5489",
            true
        );        

        System.Console.WriteLine("Id del cliente de prueba: "+command.Id);
        
        // Act (se ejecuta el método a probar de nuestra prueba unitaria)
        var result = await _handler.Handle(command, default);

        // Assert (se verifica los datos de retorno de nuestro método probado en la prueba unitaria)
        result.IsError.Should().BeTrue(); // hay error
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Customer.CustomerWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Customer.CustomerWithBadFormat.Description);
    }

    // FUNCIONA PERO REVISAR PARA QUE DE UNA RESPUESTA INDEPENDIENTE A CADA ERROR EN ESPECÍFICO

}