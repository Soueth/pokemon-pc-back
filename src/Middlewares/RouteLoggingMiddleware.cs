namespace PokemonPc.Middlewares;

public class RouteLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RouteLoggingMiddleware> _logger;

    public RouteLoggingMiddleware(RequestDelegate next, ILogger<RouteLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"[{context.Request.Method}] {context.Request.Path}");

        await _next(context);
    }
}
