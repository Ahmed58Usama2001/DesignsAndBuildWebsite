namespace DesignsAndBuild.Core.Specifications.OurProject_Specs;

public class OurProjectSpeceficationsParams
{
    public int? duration { get; set; }

    public string? sort { get; set; }

    private string? search;

    public string? Search
    {
        get { return search; }
        set { search = value?.ToLower(); }
    }


    private const int maxPageSize = 12;
    private int pageSize = 6;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    public int PageIndex { get; set; } = 1;

}
