using PokemonPc.Middlewares;

namespace PokemonPc.Configurations;

public static class AppExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        if (env.IsDevelopment())
        {
            app.UseMiddleware<RouteLoggingMiddleware>();
        }
    }

    // public static void ConfigureWebSocket(this IApplicationBuilder app)
    // {
    //     app.Use(async (context, next) =>
    //     {
    //         if (context.WebSockets.IsWebSocketRequest)
    //         {
    //             using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
    //             // await HandleWeb
    //         }
    //         else
    //         {
    //             await next();
    //         }
    //     });
    // }
}
