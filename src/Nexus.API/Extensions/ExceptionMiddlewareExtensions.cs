using Nexus.API.Middleware;

namespace Nexus.API.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}