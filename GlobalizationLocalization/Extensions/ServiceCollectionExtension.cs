using GlobalizationLocalization.Resources;
using GlobalizationLocalization.Services;
using GlobalizationLocalization.Services.Abstracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Reflection;

namespace GlobalizationLocalization.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration Configuration)
    {
        services.RegisterServices();
        services.ConfigureLocalization();
        return services;
    }
    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<ISharedResourceService, SharedResourceService>();
        return services;
    }
    private static IServiceCollection ConfigureLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.Configure<RequestLocalizationOptions>(
                   options =>
                   {
                       var supportedCultures = new List<CultureInfo>
                           {
                                   new CultureInfo("en-US"),
                                   new CultureInfo("de-CH"),
                                   new CultureInfo("fr-CH")
                           };

                       options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                       options.SupportedCultures = supportedCultures;
                       options.SupportedUICultures = supportedCultures;
                       options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                   });

        services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                });

        return services;
    }
}