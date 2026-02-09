using DefaultNamespace;
using projectWeb.Application.DTOs.Auth;
using projectWeb.Application.Interfaces;
using AutoMapper;

namespace projectWeb.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    
    public AuthService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    }
    
    public async Task<User> RegisterAsync(RegisterUserDto registerDto)
    {
       var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
       if (existingUser != null)
       {
           throw new Exception("El usuario ya existe");
       }
       
       var user = _mapper.Map<User>(registerDto);
       user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
       
       await _userRepository.AddAsync(user);

       return user;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginUserDto loginDto)
    {
        // 1. Verificar si el usuario existe
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new Exception("Credenciales inválidas (Usuario no encontrado)");
        }

        // 2. Verificar contraseña
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
             throw new Exception("Credenciales inválidas (Contraseña incorrecta)");
        }

        // 3. Generar Access Token
        var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.email),
            // new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role.Name) // Si tu rol tiene propiedad Name
        };
        
        var accessToken = _tokenService.GenerateAccessToken(claims);

        // 4. Generar Refresh Token
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        // 5. Guardar Refresh Token en Base de Datos
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Refresh token dura 7 días
        
        await _userRepository.UpdateAsync(user); // Asumimos que tienes este método en el repositorio genérico

        // 6. Retornar respuesta
        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = _mapper.Map<UserResponseDto>(user)
        };
    }
}