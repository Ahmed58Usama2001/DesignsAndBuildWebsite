namespace DesignsAndBuild.Service.OurProjectsServices;

public class ProjectServices: IProjectServices<OurProject>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateProjectAsync(OurProject ourProject)
    {
        try
        {
            await _unitOfWork.Repository<OurProject>().AddAsync(ourProject);

            
            foreach(var img in ourProject.Imags)
            {
                 await _unitOfWork.Repository<ProjectImags>().AddAsync(img);
            }

            await _unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception ex) 
        {
            return false;
        }
    }

    public async Task<bool> DeleteProjectAsync(OurProject project)
    {
        try
        {
            _unitOfWork.Repository<OurProject>().Delete(project);
            foreach (var img in project.Imags)
            {
                 _unitOfWork.Repository<ProjectImags>().Delete(img);
            }

            await _unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<IReadOnlyList<OurProject>> GetAllProjectsAsync()
        => await _unitOfWork.Repository<OurProject>().GetAllAsync();

    public async Task<OurProject>GetProjectByIdAsync(int id)
        => await _unitOfWork.Repository<OurProject>().GetByIdAsync(id);

    public async Task<bool> UpdateProject(OurProject project)
    {
        try
        {
             _unitOfWork.Repository<OurProject>().Update(project);
             await _unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
