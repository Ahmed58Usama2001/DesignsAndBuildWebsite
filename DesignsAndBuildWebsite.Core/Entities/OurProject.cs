namespace DesignsAndBuild.Core.Entities;

public class OurProject : BaseEntity
{
    public string Title { get; set; }
    public string ArabicTitle { get; set; }
    public string? ClientName { get; set; }
    public string? ArabicClientName { get; set; }
    public string Description { get; set; }
    public string ArabicDescription { get; set; }
    public string Duration { get; set; }
    public string ArabicDuration { get; set; }
    public string? VideoUrl { get; set; }
    public virtual ICollection<ProjectImags> Imags { get; set; } = new List<ProjectImags>();
}