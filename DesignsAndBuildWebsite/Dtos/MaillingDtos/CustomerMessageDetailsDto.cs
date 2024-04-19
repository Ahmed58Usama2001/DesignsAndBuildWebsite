namespace DesignsAndBuild.APIs.Dtos.MaillingDtos;

public class CustomerMessageDetailsDto
{
    [Required, MaxLength(50),MinLength(3)]
    public string? FirstName { get; set; }

    [Required, MaxLength(50), MinLength(3)]
    public string? LastName { get; set; }
    
    [DataType(DataType.EmailAddress),Required]
    public string? Email { get; set; }
    
    [Phone,Required]
    public string? Phone { get; set; }

    [Required,MinLength(20)]
    public string? Message { get; set; } 
}