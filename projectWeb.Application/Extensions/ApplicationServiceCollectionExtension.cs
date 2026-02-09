using Microsoft.Extensions.DependencyInjection;
using projectWeb.Application.Mapping;
using projectWeb.Application.Services.Auth;
using projectWeb.Application.Interfaces;

namespace projectWeb.Application.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registramos AutoMapper
        services.AddAutoMapper(typeof(MapingsProfiles).Assembly);
        
        // Registramos Servicios de Aplicación
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITitleService, Services.Title.TitleService>();
        services.AddScoped<IEpisodeService, Services.Episode.EpisodeService>();
        
        return services;
    }
}
