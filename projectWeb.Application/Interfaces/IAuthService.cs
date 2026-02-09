using DefaultNamespace;
using projectWeb.Application.DTOs.Auth;

namespace projectWeb.Application.Interfaces;

public interface IAuthService
{
    // Recibe un dto y devuelve el usario creado 

    Task<User> RegisterAsync(RegisterUserDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginUserDto loginDto); 
}