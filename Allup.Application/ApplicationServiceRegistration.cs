using Allup.Application.Services.Abstracts;
using Allup.Application.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Allup.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(
           options =>
           {
               var supportedCultures = new List<CultureInfo>
                   {
                        new CultureInfo("en-US"),
                        new CultureInfo("az"),
                   };

               options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

               options.SupportedCultures = supportedCultures;
               options.SupportedUICultures = supportedCultures;

           });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ILanguageService, LanguageManager>();

        services.AddSingleton<StringLocalizerService>();

        return services;
    }
}
