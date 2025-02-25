using Domain.Customers;
using Domain.Primitives;
using Application.Customers.Create;
using Domain.DomainErrors;
using Application.Customers.GetId;

namespace Application.Customers.UnitTests.Application.Customers.Unitests.GetById;

public class GetByIdCustomerCommandHandlerUnitTests
{
    

    /*
        NOMENCLATURA A SEGUIR:
        - ¿Qué vamos a probar?
        - El escenario
        - Lo que vamos a arrojar
    */

    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly GetIdCustomerCommandHandler _handler;

    public GetByIdCustomerCommandHandlerUnitTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();

        _handler = new GetIdCustomerCommandHandler(_mockCustomerRepository.Object);
    }

    [Fact]
    public async Task HandleGetByIdCustomer_WhenCustomerByIdHasNotFound_ShouldReturnValidationError()
    {
        // Arrange (se configura los parametros de entrada de nuestra prueba unitaria)
        GetIdCustomerCommand command = new GetIdCustomerCommand(Guid.NewGuid());
        
        // Act (se ejecuta el método a probar de nuestra prueba unitaria)
        var result = await _handler.Handle(command, default);
        
        // Assert (se verifica los datos de retorno de nuestro método probado en la prueba unitaria)
        result.IsError.Should().BeTrue(); // hay error
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Customer.CustomerByIdNotFound.Code);
        result.FirstError.Description.Should().Be(Errors.Customer.CustomerByIdNotFound.Description);
    }

    
}