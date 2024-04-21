namespace DesignsAndBuild.Core.Specifications.Contact_Specs;

public class UserMessageSpecification:BaseSpecifications<UserMessage>
{
    public UserMessageSpecification(UserMessageSpecificationParms speceficationsParams) :
        base(
                P => (string.IsNullOrEmpty(speceficationsParams.Email) || P.Email == speceficationsParams.Email)&&
                (speceficationsParams.StartDate == null || P.SendDate.Value.Date >= speceficationsParams.StartDate.Value.ToDateTime(TimeOnly.MinValue))&&
                (speceficationsParams.EndDate == null || P.SendDate <= speceficationsParams.EndDate.Value.ToDateTime(TimeOnly.MaxValue))&&
                (speceficationsParams.NotSeen == false || P.IsSeen == false)
            )
    {
        if(!string.IsNullOrEmpty(speceficationsParams.Sort))
        {
            switch (speceficationsParams.Sort)
            {
                case "Date":
                    AddOrderBy(P => P.SendDate);
                    break;
                case "DateDesc":
                    AddOrderByDesc(P => P.SendDate);
                    break;
                default:
                    AddOrderBy(P => P.SendDate);
                break;
            }
        }
        else
            AddOrderBy(P => P.SendDate);

        ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);

    }

    public UserMessageSpecification(int id)
       : base(p => p.Id == id)
    {
    }
}
