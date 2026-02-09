using DefaultNamespace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projectWeb.Infrastructure.Data;
using projectWeb.Infrastructure.Repositories;

namespace projectWeb.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("ConectionDb"),
                new MySqlServerVersion(new Version(8, 0, 2))
            )
        );
       
            
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // inyectar repsotori generico 
        services.AddScoped(typeof(IGeneral<>), typeof(GeneralRepository<>));
        
        // inyecar nuevo repositorio especifico para user
                         
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Registrar Cloudinary Service
        services.AddScoped<projectWeb.Application.Interfaces.ICloudinaryService, Services.CloudinaryService>();
        
        return services;
    }
}