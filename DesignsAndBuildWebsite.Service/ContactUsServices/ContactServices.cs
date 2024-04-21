
namespace DesignsAndBuild.Service.ContactUsServices;

public class ContactServices: IUserMessageServices
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserMessage?> CreateMessageAsync(UserMessage message)
    {
        try
        {        
            await _unitOfWork.Repository<UserMessage>().AddAsync(message);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 1) return null;

            return message;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
    public async Task<IReadOnlyList<UserMessage>> ReadAllMessagesAsync(UserMessageSpecificationParms speceficationsParams)
    {
        var spec = new UserMessageSpecification(speceficationsParams);

        try
        {
            var messages = await _unitOfWork.Repository<UserMessage>().GetAllWithSpecAsync(spec);

            if (messages is null) return null;

            return messages;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<UserMessage?> ReadMessageByIdAsync(int messageId)
    {
        var spec = new UserMessageSpecification(messageId);

        try
        {
            var message = await _unitOfWork.Repository<UserMessage>().GetByIdWithSpecAsync(spec);

            if (message is null) return null;

            return message;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteMessage(UserMessage message)
    {
        try
        {

            _unitOfWork.Repository<UserMessage>().Delete(message);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

    public async Task<int> GetCountAsync(UserMessageSpecificationParms speceficationsParams)
    {
        var countSpec = new UserMessageWithFilterationForCountSpecifications(speceficationsParams);

        var count = await _unitOfWork.Repository<UserMessage>().GetCountAsync(countSpec);

        return count;
    }

}
