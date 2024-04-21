namespace DesignsAndBuild.APIs.Controllers;

public class OurProjectController : BaseApiController
{
    private readonly IProjectServices _projectService;
    private readonly IMapper _mapper;

    public OurProjectController(IProjectServices projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(OurProjectReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<OurProjectReturnDto>> CreateProject(OurProjectCreateDto projectDto)
    {
        if (projectDto is null) return BadRequest(new ApiResponse(400));

        var project = _mapper.Map<OurProjectCreateDto, OurProject>(projectDto);

        foreach (var image in projectDto.Images)
        {
            var imageUrl = DocumentSetting.UploadFile(image, "Imgs\\ProjectsImages");
            OurProjectImages img = new OurProjectImages() { PictureUrl = imageUrl };
            project?.Images?.Add(img);
        }

        if (!string.IsNullOrEmpty(project?.VideoUrl))
            project.VideoUrl = DocumentSetting.UploadFile(projectDto?.Video, "Videos\\ProjectsVideos");

        var createdProject = await _projectService.CreateProjectAsync(project);

        if (createdProject is null)
            return BadRequest(new ApiResponse(400));
        else
            return Ok(_mapper.Map<OurProject, OurProjectReturnDto>(createdProject));
    }

    [ProducesResponseType(typeof(OurProjectReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<Pagination<OurProjectReturnDto>>> GetAllProjects([FromQuery] OurProjectSpeceficationsParams speceficationsParams)
    {
        var projects = await _projectService.ReadAllProjectsAsync(speceficationsParams);

        if (projects == null)
            return NotFound(new ApiResponse(404));

        var count = await _projectService.GetCountAsync(speceficationsParams);

        var data = _mapper.Map<IReadOnlyList<OurProject>, IReadOnlyList<OurProjectReturnDto>>(projects);

        return Ok(new Pagination<OurProjectReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(OurProjectReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OurProjectReturnDto>> GetProjectById(int id)
    {
        var project = await _projectService.ReadProjectByIdAsync(id);
        if (project == null) return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<OurProjectReturnDto>(project));
    }

    [ProducesResponseType(typeof(OurProjectReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{projectId}")]
    public async Task<ActionResult<OurProjectReturnDto>> UpdateProject(int projectId, OurProjectCreateDto updatedProjectDto)
    {
        var storedProject = await _projectService.ReadProjectByIdAsync(projectId);

        foreach (var img in storedProject.Images)
        {
            DocumentSetting.DeleteFile(img.PictureUrl);
        }

        if (!string.IsNullOrEmpty(storedProject.VideoUrl))
            DocumentSetting.DeleteFile(storedProject.VideoUrl);

        var newProject = _mapper.Map<OurProjectCreateDto, OurProject>(updatedProjectDto);
        newProject.Id = storedProject.Id;

        foreach (var image in updatedProjectDto.Images)
        {
            var imageUrl = DocumentSetting.UploadFile(image, "Imgs\\ProjectsImages");
            OurProjectImages img = new OurProjectImages() { PictureUrl = imageUrl };
            newProject?.Images?.Add(img);
        }

        if (!string.IsNullOrEmpty(newProject?.VideoUrl))
            newProject.VideoUrl = DocumentSetting.UploadFile(updatedProjectDto?.Video, "Videos\\ProjectsVideos");

        storedProject = await _projectService.UpdateProject(storedProject, newProject);

        if (storedProject == null)
            return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<OurProjectReturnDto>(storedProject));
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _projectService.ReadProjectByIdAsync(id);

        if (project is null)
            return NotFound(new ApiResponse(404));

        var result =await _projectService.DeleteProject(project);

        if (result)
        {
            foreach (var img in project.Images)
            {
                DocumentSetting.DeleteFile(img.PictureUrl);
            }

            if (!string.IsNullOrEmpty(project.VideoUrl))
                DocumentSetting.DeleteFile(project.VideoUrl);


            return Ok(true);
        }

        return BadRequest(new ApiResponse(400));
    }
}