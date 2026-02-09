namespace projectWeb.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public required UserResponseDto User { get; set; } // Reutilizamos el DTO de respuesta seguro
}
