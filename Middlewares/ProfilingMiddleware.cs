using System.Diagnostics;

namespace PokemonReviewApp.Middlewares;

public class ProfilingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ProfilingMiddleware> _logger;

    public ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        
        await _next(context);
        
        watch.Stop();
        _logger.LogInformation($"Request `{context.Request.Path}` took `{watch.ElapsedMilliseconds}`");
    }
}
