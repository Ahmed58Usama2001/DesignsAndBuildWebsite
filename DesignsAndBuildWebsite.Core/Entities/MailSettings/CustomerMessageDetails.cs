namespace DesignsAndBuild.Core.Entities.MailSettings;

public class CustomerMessageDetails :BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
    public DateTime? SendMessageDate { get; set; }
    public bool IsSeened { get; set; }
    public DateTime? DateMessageSeenAt { get; set; }
    public string SeenByWho { get; set; }
    public CustomerMessageDetails()
    {
        SendMessageDate = DateTime.UtcNow;
    }
}