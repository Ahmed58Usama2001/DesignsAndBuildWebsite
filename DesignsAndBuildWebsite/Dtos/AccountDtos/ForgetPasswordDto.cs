namespace DesignsAndBuild.APIs.Dtos.AccountDtos;

public class ForgetPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
