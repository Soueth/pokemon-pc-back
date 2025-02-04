using System.Text.Json;
using PokemonPc.Constants;
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

        bool isDefault = false;

        var statusCode = exception switch
        {
            EmailJaCadastradoException => StatusCodes.Status400BadRequest,
            EmailSenhaIncorretosException => StatusCodes.Status401Unauthorized,
            EmptyIdException => StatusCodes.Status500InternalServerError,
            _ => ( isDefault = true, StatusCodes.Status500InternalServerError ).Item2,
        };

        context.Response.StatusCode = statusCode;

        string error = exception.Message;

        if (isDefault)
        {
            error = AppConstants.ERROR_MESSAGES.UNEXPECTED_ERROR;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro inesperado: \n {exception}");
            Console.ResetColor();
        }

        var response = new
        {
            error,
            status = statusCode
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
