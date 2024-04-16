namespace DesignsAndBuild.Core.Entities.Identity.Gmail;

public class GoogleSignInDto
{
    [Required]
    public string IdToken { get; set; }
}
