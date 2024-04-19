using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignsAndBuild.Core.Specifications
{
    public class MessageSpecificationParms 
    {
        public const int MaxPageSize = 10;
        private int? pageSize = 5;
        public int? PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public int? PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public string? Email { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool? NotSeened { get; set; }
    }
}
