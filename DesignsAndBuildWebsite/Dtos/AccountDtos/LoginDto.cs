﻿namespace DesignsAndBuild.APIs.Dtos.AccountDtos;

public class LoginDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
