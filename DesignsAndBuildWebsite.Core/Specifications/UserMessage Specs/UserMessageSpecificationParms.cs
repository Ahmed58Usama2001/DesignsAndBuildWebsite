namespace DesignsAndBuild.Core.Specifications.Contact_Specs;

public class UserMessageSpecificationParms
{
    private const int maxPageSize = 12;
    private int pageSize = 6;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    public int PageIndex { get; set; } = 1;
    public string? Sort { get; set; }
    public string? Email { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool? NotSeen { get; set; }
}
