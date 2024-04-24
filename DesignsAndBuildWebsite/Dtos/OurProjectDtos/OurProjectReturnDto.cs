namespace DesignsAndBuild.APIs.Dtos.ProjectDtos;

public class OurProjectReturnDto
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string ArabicTitle { get; set; }

    public string? ClientName { get; set; }
    public string? ArabicClientName { get; set; }

    public string Description { get; set; }
    public string ArabicDescription { get; set; }

    public string Location { get; set; }
    public string ArabicLocation { get; set; }

    public string Year { get; set; }

    public string VideoUrl { get; set; }

    public IEnumerable<string> ImageUrls { get; set; } = new List<string>();

}
