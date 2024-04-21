namespace DesignsAndBuild.Core.Specifications.OurProject_Specs;

public class OurProjectWithFilterationForCountSpecifications : BaseSpecifications<OurProject>
    {
        public OurProjectWithFilterationForCountSpecifications(OurProjectSpeceficationsParams speceficationsParams) :
              base(p =>
              (!speceficationsParams.duration.HasValue || p.DurationInDays <= speceficationsParams.duration)
            &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || p.Title.ToLower().Contains(speceficationsParams.Search)
              || p.ArabicTitle.ToLower().Contains(speceficationsParams.Search)
              || p.Description.ToLower().Contains(speceficationsParams.Search)
              || p.ArabicDescription.ToLower().Contains(speceficationsParams.Search)
               || p.ClientName.ToLower().Contains(speceficationsParams.Search) 
               || p.ArabicClientName.ToLower().Contains(speceficationsParams.Search) 
            ))
        {

        }
    }

