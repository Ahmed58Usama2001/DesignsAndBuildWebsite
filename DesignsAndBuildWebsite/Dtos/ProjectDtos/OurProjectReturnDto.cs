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
    public string? Duration { get; set; }
    public string ArabicDuration { get; set; }
    public IEnumerable<string> ImageUrl { get; set; } = new List<string>();
    public string VideoUrl { get; set; }
}
