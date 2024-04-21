namespace DesignsAndBuild.APIs.Dtos.ProjectDtos;

public class OurProjectCreateDto
{
    public string Title { get; set; }
    public string ArabicTitle { get; set; }

    public string? ClientName { get; set; }
    public string? ArabicClientName { get; set; }

    public string Description { get; set; }
    public string ArabicDescription { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public IFormFile? Video { get; set; }

    public List<IFormFile> Images { get; set; }


}
