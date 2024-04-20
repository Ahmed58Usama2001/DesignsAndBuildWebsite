namespace DesignsAndBuild.APIs.Controllers;

public class OurProjectController : BaseApiController
{
    private readonly IProjectServices<OurProject> _projectService;
    private readonly IMapper _mapper;

    public OurProjectController(IProjectServices<OurProject> projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(OurProjectDto projectDetalis)
    {
        if (projectDetalis == null) return BadRequest(new ApiResponse(400));

        var project = _mapper.Map<OurProjectDto, OurProject>(projectDetalis);

        foreach (var imgage in projectDetalis.Image)
        {
            var imgeUrl = DocumentSetting.UpLoadFile(imgage, "Imgs\\ProjectsImages");
            ProjectImags img = new ProjectImags() { PictureUrl = imgeUrl };
            project.Imags.Add(img);
        }

        project.VideoUrl = DocumentSetting.UpLoadFile(projectDetalis.Video, "Videos\\ProjectsVideos");

        var result = await _projectService.CreateProjectAsync(project);

        if (result)
            return Ok(result);
        else
            return
                BadRequest(new ApiResponse(400));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var project = await _projectService.GetAllProjectsAsync();
        var mappedProject = _mapper.Map<IEnumerable<OurProject>, IEnumerable<OurProjectReturnDto>>(project);
        return Ok(mappedProject);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null) return Ok(new ApiResponse(404));

        var mappedProject = _mapper.Map<OurProject, OurProjectReturnDto>(project);
        return Ok(mappedProject);
    }

    [HttpPut]
    public IActionResult UpdateProject(OurProjectUpdatedDto project)
    {
        //var storedProject = _projectService.GetProjectByIdAsync(project.Id);

        var mappedProject = _mapper.Map<OurProjectUpdatedDto, OurProject>(project);

        var result =  _projectService.UpdateProject(mappedProject);

        if (result.IsCompleted) return Ok(result);

        return BadRequest(new ApiResponse(400));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id) 
    {
        
        var project = await _projectService.GetProjectByIdAsync(id);

        if (project == null) return Ok(new ApiResponse(404));

        foreach(var img in project.Imags)
        {
            DocumentSetting.DeleteFile(img.PictureUrl);
        }

        if(!string.IsNullOrEmpty(project.VideoUrl))
            DocumentSetting.DeleteFile(project.VideoUrl);

        var result = await _projectService.DeleteProjectAsync(project);
       
        if (result) return Ok(result);

        return BadRequest(new ApiResponse(400));
    }
}