

namespace DesignsAndBuild.Core.Services.Contract.ContactUsDomainContract;

public interface IUserMessageServices
{
    Task<UserMessage?> CreateMessageAsync(UserMessage message);

    Task<IReadOnlyList<UserMessage>> ReadAllMessagesAsync(UserMessageSpecificationParms speceficationsParams);

    Task<UserMessage?> ReadMessageByIdAsync(int messageId);

    Task<bool> DeleteMessage(UserMessage message);

    Task<int> GetCountAsync(UserMessageSpecificationParms speceficationsParams);

}
