using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Common.RequestResult;
using System.Globalization;
using System.Text.Json;

namespace Auth.VirtualCare.Application.Common.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IRequestResult
{
    private readonly IValidator<TRequest>? _validator;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
    public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IValidator<TRequest>? validator = null)
    {
        _validator = validator;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es-ES");

        if (_validator is null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        var errors = validationResult.Errors
            .Select(validationFailure => $"{validationFailure.PropertyName}: {validationFailure.ErrorMessage}")
            .ToList();

        // Crear una instancia de RequestResult directamente
        var errorResult = new RequestResult(null, false, "Validaciones: " + string.Join(',', errors), typeof(TRequest).Name);

        _logger.LogWarning("Warning validación: " + JsonSerializer.Serialize(errorResult));

        // Convertir el errorResult a TResponse
        return (TResponse)(object)errorResult;
    }

}
