using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Common.Behavior;

// Implemente el PipelineBehavior donde la petición es de MediatR pero la respuesta es de IErrorOr
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
 where TRequest : IRequest<TResponse>
 where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if(_validator is null) {
            // continua la validacion
            return await next();
        }

        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if(validatorResult.IsValid) {
            return await next();
        }

        // Convertimos los errores de FluentValidation a errores de IErrorOr
        // Le pasamos el nombre de la propiedad como código y el mensaje de error como descripción
        var errors = validatorResult.Errors
                    .ConvertAll(validationFailure => Error.Validation(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage
                    ));

        return (dynamic)errors;
    }
}