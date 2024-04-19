using System.Threading.Tasks;
namespace DesignsAndBuild.Core.Mail.Contract;

public interface IMaillingService
{
    Task SendEmailAsync(string mailTo, string subject, string body);
}