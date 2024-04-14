namespace DesignsAndBuild.APIs.Dtos.AccountDtos;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
