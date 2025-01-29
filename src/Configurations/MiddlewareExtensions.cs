using PokemonPc.Middlewares;

namespace PokemonPc.Configurations;

public static class MiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
