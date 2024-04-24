using System.ComponentModel.DataAnnotations.Schema;

namespace DesignsAndBuild.Core.Entities.OurProjectDomainEntity;

public class OurProject : BaseEntity
{
    public string Title { get; set; }
    public string ArabicTitle { get; set; }
    
    public string? ClientName { get; set; }
    public string? ArabicClientName { get; set; }
    
    public string Description { get; set; }
    public string ArabicDescription { get; set; }
    
    public string Year { get; set; }

    public string Location { get; set; }
    public string ArabicLocation { get; set; }
   
    public string? VideoUrl { get; set; }

    public virtual ICollection<OurProjectImages>? Images { get; set; } = new List<OurProjectImages>();
}