namespace DesignsAndBuild.APIs.Helpers;

public static class EmailSettings
{
    public static void SendEmail(Email email)
    {
        var client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential("ahmedusamasaad@gmail.com", "Osama58200165");
        client.Send("ahmedusamasaad@gmail.com", email.To, email.Title, email.Body);
    }
}