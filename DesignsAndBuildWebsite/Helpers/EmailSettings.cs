namespace DesignsAndBuild.APIs.Helpers;

public static class EmailSettings
{
    public static void SendEmail(Email email)
    {
        var client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential("DesignsandBuild1@gmail.com", "Password");
        client.Send("DesignsandBuild1@gmail.com", email.To, email.Title, email.Body);
    }
}