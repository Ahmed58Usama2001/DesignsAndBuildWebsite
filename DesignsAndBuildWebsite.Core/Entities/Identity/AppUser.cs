namespace DesignsAndBuild.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? ProfilePictureUrl { get; set; }

    public DateTime RegistrationDate { get; set; }
}
