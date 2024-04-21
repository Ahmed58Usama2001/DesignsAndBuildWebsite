namespace DesignsAndBuild.APIs.Controllers;

public class UserMessageController : BaseApiController
{
    private readonly IUserMessageServices _contactServices;
    private readonly IMapper _mapper;

    public UserMessageController(IUserMessageServices  contactServices, IMapper mapper)
    {

        _contactServices = contactServices;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(UserMessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<OurProjectReturnDto>> SendMessage([FromForm] UserMessageDto customerMessage)
    {
        if (customerMessage is null) return BadRequest(new ApiResponse(400));

        var createdMessage = _mapper.Map<UserMessageDto, UserMessage>(customerMessage);

         createdMessage = await _contactServices.CreateMessageAsync(createdMessage);

        if(createdMessage is not null)
        return Ok(_mapper.Map<UserMessage, UserMessageDto>(createdMessage));

        return BadRequest(new ApiResponse(400));
      
    }

    [ProducesResponseType(typeof(UserMessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("AllMessages")]
    public async Task<ActionResult<Pagination<UserMessageDto>>> GetAllMessages([FromQuery] UserMessageSpecificationParms speceficationsParams)
    {
        var messages = await _contactServices.ReadAllMessagesAsync(speceficationsParams);

        if (messages == null)
            return NotFound(new ApiResponse(404));

        var count = await _contactServices.GetCountAsync(speceficationsParams);

        var data = _mapper.Map<IReadOnlyList<UserMessage>, IReadOnlyList<UserMessageDto>>(messages);

        return Ok(new Pagination<UserMessageDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(UserMessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{messageId}")]
    public async Task<ActionResult<UserMessageDto>> GetMessageById(int messageId)
     {
        var message = await _contactServices.ReadMessageByIdAsync(messageId);
        if (message == null) return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<UserMessageDto>(message));
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{messageId}")]
    public async Task<IActionResult> DeleteMessage(int messageId)
    {
        var message = await _contactServices.ReadMessageByIdAsync(messageId);

        if (message is null)
            return NotFound(new ApiResponse(404));

        var result = await _contactServices.DeleteMessage(message);

        if (result)       
           return Ok(true);
        
        return BadRequest(new ApiResponse(400));
    }
}