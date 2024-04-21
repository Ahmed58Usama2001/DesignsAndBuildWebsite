

namespace DesignsAndBuild.Core.Services.Contract.ContactUsDomainContract;

public interface IProjectServices
{
    Task<OurProject?> CreateProjectAsync(OurProject contact);

    Task<IReadOnlyList<OurProject>> ReadAllProjectsAsync(OurProjectSpeceficationsParams speceficationsParams);

    Task<OurProject?> ReadProjectByIdAsync(int projectId);

    Task<OurProject?> UpdateProject(OurProject storedProject, OurProject newProject);


    Task<bool> DeleteProject(OurProject project);

    Task<int> GetCountAsync(OurProjectSpeceficationsParams speceficationsParams);

}
