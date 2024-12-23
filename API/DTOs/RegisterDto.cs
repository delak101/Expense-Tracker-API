﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    [MaxLength(28)]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}