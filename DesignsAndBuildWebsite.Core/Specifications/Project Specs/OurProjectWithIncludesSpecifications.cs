
namespace DesignsAndBuild.Core.Specifications.OurProject_Specs;

public class OurProjectWithIncludesSpecifications : BaseSpecifications<OurProject>
{
    public OurProjectWithIncludesSpecifications(OurProjectSpeceficationsParams speceficationsParams)
        : base(p =>
              
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || p.Title.ToLower().Contains(speceficationsParams.Search)
              || p.ArabicTitle.ToLower().Contains(speceficationsParams.Search)
              || p.Description.ToLower().Contains(speceficationsParams.Search)
              || p.ArabicDescription.ToLower().Contains(speceficationsParams.Search)
               || p.ClientName.ToLower().Contains(speceficationsParams.Search)
               || p.ArabicClientName.ToLower().Contains(speceficationsParams.Search)
            ))

    {
        AddIncludes();

        ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);
    }

    public OurProjectWithIncludesSpecifications(int id)
        :base(p=>p.Id==id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(p => p.Images);
    }

}
