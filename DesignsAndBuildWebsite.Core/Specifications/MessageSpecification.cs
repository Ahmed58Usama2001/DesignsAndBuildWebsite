using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignsAndBuild.Core.Specifications
{
    public class MessageSpecification:BaseSpecifications<CustomerMessageDetails>
    {
        public MessageSpecification(MessageSpecificationParms parms):
            base(
                    P => (string.IsNullOrEmpty(parms.Email) || P.Email == parms.Email)&&
                    (parms.StartDate == null || P.SendMessageDate.Value.Date >= parms.StartDate.Value.ToDateTime(TimeOnly.MinValue))&&
                    (parms.EndDate == null || P.SendMessageDate <= parms.EndDate.Value.ToDateTime(TimeOnly.MaxValue))&&
                    (parms.NotSeened == false || P.IsSeened == false)
                )
        {
            if(!string.IsNullOrEmpty(parms.Sort))
            {
                switch (parms.Sort)
                {
                    case "Date":
                        AddOrderBy(P => P.SendMessageDate);
                        break;
                    case "DateDesc":
                        AddOrderByDesc(P => P.SendMessageDate);
                        break;
                    default:
                        AddOrderBy(P => P.SendMessageDate);
                    break;
                }
            }
            else
                AddOrderBy(P => P.SendMessageDate);

            if (parms.PageSize is not null && parms.PageIndex is not null)
            { 
                ApplyPagination((parms.PageIndex-1)*parms.PageSize, parms.PageSize);
            }
        }
    }
}
