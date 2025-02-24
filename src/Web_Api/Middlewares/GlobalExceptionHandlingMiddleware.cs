
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Middleware;

// solo va a capturar las excepciones que no podamos controlar
public class GlobalExceptionHandlerMiddleware : IMiddleware
{
private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    // Maneja toodas las peticiones http que entren por el servidor API
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try {
            await next(context);
        } catch (Exception e) {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new() {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server has ocurred."
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}