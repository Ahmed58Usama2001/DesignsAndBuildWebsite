namespace DesignsAndBuild.Core.Entities.MailSettings;

public class UserMessage :BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Message { get; set; }

    public bool IsSeen { get; set; }

    public DateTime? SeenDate { get; set; }
    public DateTime? SendDate { get; set; }=DateTime.UtcNow;
   
}