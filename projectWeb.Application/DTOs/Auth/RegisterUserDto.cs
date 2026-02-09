using System.ComponentModel.DataAnnotations;

namespace projectWeb.Application.DTOs.Auth;

public class RegisterUserDto
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    
}