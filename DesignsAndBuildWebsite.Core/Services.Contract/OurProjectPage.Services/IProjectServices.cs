using DesignsAndBuild.Core.Specifications.OurProject_Specs;

namespace DesignsAndBuild.Core.Services.Contract.OurProjectPage.Services;

public interface IOurProjectServices 
{
     Task<OurProject?> CreateProjectAsync(OurProject ourProject);

    Task<IReadOnlyList<OurProject>> ReadAllProjectsAsync(OurProjectSpeceficationsParams speceficationsParams);

     Task<OurProject?> ReadProjectByIdAsync(int projectId);

    Task<OurProject?> UpdateSubject(OurProject storedProject, OurProject newProject);

    Task<bool> DeleteSubject(OurProject project);

    Task<int> GetCountAsync(OurProjectSpeceficationsParams speceficationsParams);

}
