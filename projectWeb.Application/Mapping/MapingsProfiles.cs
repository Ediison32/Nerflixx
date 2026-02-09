using AutoMapper;
using DefaultNamespace;
using projectWeb.Application.DTOs.Auth;

namespace projectWeb.Application.Mapping;

public class MapingsProfiles : Profile
{
    public MapingsProfiles()
    {
        // Regla: Convierte RegisterUserDto a User
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // ¡OJO! La contraseña se mapea manual porque hay que hashearla
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())) // Generar ID automático
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Role.User)); // Rol por defecto

        // Regla de Salida: Convierte User (BD) a UserResponseDto (Cliente)
        CreateMap<User, UserResponseDto>();
    }
}