namespace DesignsAndBuild.APIs.Dtos.ProjectDtos;

public class OurProjectCreateDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string ArabicTitle { get; set; }

    [Required]
    public string? ClientName { get; set; }
    [Required]
    public string? ArabicClientName { get; set; }

    [Required]
    public string Description { get; set; }
    [Required]
    public string ArabicDescription { get; set; }
    
    [Required]
    public string Location { get; set; }
    [Required]
    public string ArabicLocation { get; set; }

    [Required]
    public string Year { get; set; }

    public IFormFile? Video { get; set; }

    [Required]
    public List<IFormFile> Images { get; set; }


}
