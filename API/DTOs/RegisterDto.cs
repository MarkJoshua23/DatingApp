using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    public required string Username { get; set; }

    //other way of making a prop required
    [Required]
    public required string Password { get; set; }
}
