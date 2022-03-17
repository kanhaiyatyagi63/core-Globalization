using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Localization.Midlewares;

namespace GlobalizationLocalization.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseInfrastructureApplicationBuilder(this IApplicationBuilder app)
    {
        app.UseLocalization();
        app.UseGlobalErrorHandler();
        return app;
    }
    private static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
        if (locOptions is null)
            return app;
        app.UseRequestLocalization(locOptions.Value);
        return app;
    }
    private static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalErrorHandler>();
        return app;
    }
}
