namespace DesignsAndBuild.Core.Services.Contract.OurProjectPage.Services;

public interface IProjectServices<T> where T : BaseEntity
{
    public  Task<bool> CreateProjectAsync(T ourProject);

    public Task<IReadOnlyList<T>> GetAllProjectsAsync();

    public Task<T> GetProjectByIdAsync(int id);
    public Task<bool> UpdateProject(OurProject project);
    public Task<bool> DeleteProjectAsync(OurProject project);
}
