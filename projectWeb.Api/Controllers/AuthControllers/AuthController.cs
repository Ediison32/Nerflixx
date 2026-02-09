using Microsoft.AspNetCore.Mvc;
using projectWeb.Application.DTOs.Auth;
using projectWeb.Application.Interfaces;

namespace projectWeb.Api.Controllers.AuthControllers;

[Route("api/auth")] // Agrupamos todo bajo /api/auth
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto request)
    {
        // Delegamos al servicio. Si falla, el ExceptionHandlingMiddleware se encarga.
        var response = await _authService.LoginAsync(request);
        return Ok(response);
    }
}
