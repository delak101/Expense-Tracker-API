using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    [MaxLength(28)]
    public required string Username { get; set; }
    public required string Password { get; set; }
}