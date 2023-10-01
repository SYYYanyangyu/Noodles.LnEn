using Microsoft.AspNetCore.Builder;
using Noodles.EventBus;

namespace CommonInitializer;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDefault(this IApplicationBuilder app)
    {
        app.UseEventBus();
        app.UseCors();//启用Cors
        app.UseForwardedHeaders();
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}