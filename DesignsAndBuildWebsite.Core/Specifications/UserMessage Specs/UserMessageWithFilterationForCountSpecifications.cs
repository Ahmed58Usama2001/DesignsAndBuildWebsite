namespace DesignsAndBuild.Core.Specifications.Contact_Specs;

public class UserMessageWithFilterationForCountSpecifications : BaseSpecifications<UserMessage>
    {
        public UserMessageWithFilterationForCountSpecifications(UserMessageSpecificationParms speceficationsParams) :
             base(
                P => (string.IsNullOrEmpty(speceficationsParams.Email) || P.Email == speceficationsParams.Email) &&
                (speceficationsParams.StartDate == null || P.SendDate.Value.Date >= speceficationsParams.StartDate.Value.ToDateTime(TimeOnly.MinValue)) &&
                (speceficationsParams.EndDate == null || P.SendDate <= speceficationsParams.EndDate.Value.ToDateTime(TimeOnly.MaxValue)) &&
                (speceficationsParams.NotSeen == false || P.IsSeen == false)
            )
        {

        }
    }

