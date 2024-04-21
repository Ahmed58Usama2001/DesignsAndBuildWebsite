namespace DesignsAndBuild.Service.OurProjectsServices;

public class OurProjectServices: IOurProjectServices
{
    private readonly IUnitOfWork _unitOfWork;

    public OurProjectServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OurProject?> CreateProjectAsync(OurProject ourProject)
    {
        try
        {
            foreach (var img in ourProject.Images)
            {
                await _unitOfWork.Repository<OurProjectImages>().AddAsync(img);
            }

            await _unitOfWork.Repository<OurProject>().AddAsync(ourProject);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 1) return null;

            return ourProject;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IReadOnlyList<OurProject>> ReadAllProjectsAsync(OurProjectSpeceficationsParams speceficationsParams)
    {
        var spec = new OurProjectWithIncludesSpecifications(speceficationsParams);

        try
        {
            var projects = await _unitOfWork.Repository<OurProject>().GetAllWithSpecAsync(spec);

            if (projects is null) return null;

            return projects;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }

    }

    public async Task<OurProject?> ReadProjectByIdAsync(int projectId)
    {
        var spec = new OurProjectWithIncludesSpecifications(projectId);

        try
        {
            var project = await _unitOfWork.Repository<OurProject>().GetByIdWithSpecAsync(spec);

            if (project is null) return null;

            return project;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<OurProject?> UpdateSubject(OurProject storedProject, OurProject newProject)
    {

        if (newProject == null || storedProject == null)
            return null;

        storedProject.Id=newProject.Id;
        storedProject.Title=newProject.Title;
        storedProject.Description=newProject.Description;
        storedProject.ClientName = newProject.ClientName;
        storedProject.ArabicTitle=newProject.ArabicTitle;
        storedProject.ArabicClientName=newProject.ArabicClientName;
        storedProject.ArabicDescription=newProject.ArabicDescription;
        storedProject.Images = newProject.Images;
        storedProject.StartDate=newProject.StartDate;
        storedProject.EndDate=newProject.EndDate;
        storedProject.VideoUrl=newProject.VideoUrl;

        try
        {
            // Not Sure if it's automatically updated
            //foreach (var img in project.Images)
            //{
            //    _unitOfWork.Repository<OurProjectImages>().Update(img);
            //}

            _unitOfWork.Repository<OurProject>().Update(storedProject);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return storedProject;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteSubject(OurProject project)
    {

        try
        {
            // Not Sure if it's automatically deleted
            //foreach (var img in project.Images)
            //{
            //    _unitOfWork.Repository<OurProjectImages>().Delete(img);
            //}

            _unitOfWork.Repository<OurProject>().Delete(project);

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

    public async Task<int> GetCountAsync(OurProjectSpeceficationsParams speceficationsParams)
    {
        var countSpec = new OurProjectWithFilterationForCountSpecifications(speceficationsParams);

        var count = await _unitOfWork.Repository<OurProject>().GetCountAsync(countSpec);

        return count;
    }
}
