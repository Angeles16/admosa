// Http/Middleware/GlobalExceptionHandler.cs
using admosa.Domain.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace admosa.Adapters.Http.Middleware;

public class ExceptionHandling : IExceptionHandler
{
    private readonly ILogger<ExceptionHandling> _logger;

    public ExceptionHandling(ILogger<ExceptionHandling> logger) => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, Exception exception, CancellationToken ct)
    {
        ProblemDetails problem;

        if (exception is AppException app)
        {
            _logger.LogWarning("{Code}: {Message}", app.Code, app.Message);

            problem = new ProblemDetails
            {
                Status = ToStatusCode(app.Type),
                Title = app.Type.ToString(),
                Detail = app.Message,
                Extensions = { ["code"] = app.Code }
            };
        }
        else
        {
            // Error inesperado: se registra completo, pero al cliente no se le expone el detalle
            _logger.LogError(exception, "Excepción no controlada");

            problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error interno del servidor",
                Detail = "Ocurrió un error inesperado.",
                Extensions = { ["code"] = "error.inesperado" }
            };
        }

        context.Response.StatusCode = problem.Status!.Value;
        await context.Response.WriteAsJsonAsync(
            problem, options: null, contentType: "application/problem+json", cancellationToken: ct);

        return true;
    }

    private static int ToStatusCode(ErrorType type) => type switch
    {
        ErrorType.Validation   => StatusCodes.Status400BadRequest,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Forbidden    => StatusCodes.Status403Forbidden,
        ErrorType.NotFound     => StatusCodes.Status404NotFound,
        ErrorType.Conflict     => StatusCodes.Status409Conflict,
        _                      => StatusCodes.Status500InternalServerError
    };
}