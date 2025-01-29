using System.Text.Json;
using PokemonPc.Utils.Exceptions;

namespace PokemonPc.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            EmailJaCadastradoException => StatusCodes.Status400BadRequest,
            EmailSenhaIncorretosException => StatusCodes.Status401Unauthorized,
            EmptyIdException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };

        context.Response.StatusCode = statusCode;

        var response = new
        {
            error = exception.Message,
            status = statusCode
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
