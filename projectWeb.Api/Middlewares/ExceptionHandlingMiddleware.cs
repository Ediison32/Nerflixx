using System.Net;
using System.Text.Json;

namespace projectWeb.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió una excepción no controlada.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        // Por defecto, asumimos error interno 500
        var response = new 
        { 
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "Ocurrió un error interno en el servidor.",
            Detailed = exception.Message // Ojo: En producción no deberíamos mostrar esto siempre
        };

        // Manejo específico por tipo de excepción
        if (exception is KeyNotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            response = new 
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "Recurso no encontrado.",
                Detailed = exception.Message
            };
        }
        else if (exception is ArgumentException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response = new 
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Solicitud inválida.",
                Detailed = exception.Message
            };
        }
        else if (exception.Message.Contains("El usuario ya existe"))
        {
             context.Response.StatusCode = (int)HttpStatusCode.Conflict;
             response = new 
             {
                 StatusCode = (int)HttpStatusCode.Conflict,
                 Message = exception.Message,
                 Detailed = exception.Message
             };
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
