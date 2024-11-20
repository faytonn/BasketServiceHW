using Allup.Persistence.Context;
using Allup.Persistence.Repositories.Abstraction;
using Allup.Persistence.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Allup.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AllupDbConnection"));
        });

        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<DataInitializer>();

        return services;
    }
}
