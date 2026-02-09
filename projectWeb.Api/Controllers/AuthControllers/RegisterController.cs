using Microsoft.AspNetCore.Mvc;
using projectWeb.Application.DTOs.Auth;
using projectWeb.Application.Interfaces;
using AutoMapper;

namespace projectWeb.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RegisterController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
   
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {
        // 1. Llamar al servicio que crea el usuario
        var userCreated = await _authService.RegisterAsync(request);
        
        // 2. Usar Mapper para convertir la Entidad (User) a DTO de Respuesta (UserResponseDto)
        // Esto evita exponer datos sensibles como PasswordHash
        var response = _mapper.Map<UserResponseDto>(userCreated);

        // 3. Devolver 201 Created
        return CreatedAtAction(nameof(Register), new { id = response.Id }, response);
    }
}