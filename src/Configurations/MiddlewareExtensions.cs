using PokemonPc.Middlewares;

namespace PokemonPc.Configurations;

public static class MiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        if (env.IsDevelopment())
        {
            app.UseMiddleware<RouteLoggingMiddleware>();
        }
    }
}
