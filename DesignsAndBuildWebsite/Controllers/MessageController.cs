namespace DesignsAndBuild.APIs.Controllers;

public class MessageController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MessageController(IUnitOfWork unitOfWork, IMapper mapper)
    {

        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromForm] CustomerMessageDetailsDto customerMessage)
    {
        if (customerMessage is null) return BadRequest(new ApiResponse(400));
        try
        {
            await _unitOfWork.Repository<CustomerMessageDetails>().AddAsync(_mapper.Map<CustomerMessageDetailsDto, CustomerMessageDetails>(customerMessage));
            await _unitOfWork.CompleteAsync();
            return Ok(customerMessage);
        }
        catch (Exception ex)
        {
            return BadRequest(customerMessage);
        }
    }

    [HttpGet("AllMessages")]
    public async Task<IActionResult> GetAllMessagesWithSpecifications([FromQuery] MessageSpecificationParms parms)
    {
        var messageSpec = new MessageSpecification(parms);

        var customerMessageDetails = await _unitOfWork.Repository<CustomerMessageDetails>().GetAllWithSpecAsync(messageSpec);

        return Ok(customerMessageDetails);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMessage(int messageId)
    {
        var message = GetMessageById(messageId);
        if (message is null) return BadRequest(new ApiResponse(400));
        else
        {
            _unitOfWork.Repository<CustomerMessageDetails>().Delete(message.Result);
            await _unitOfWork.CompleteAsync();
        }

        return Ok(new());
    }

    [HttpGet]
    public async Task<CustomerMessageDetails> GetMessageById(int messageId)
     => await _unitOfWork.Repository<CustomerMessageDetails>().GetByIdAsync(messageId)??new();
}