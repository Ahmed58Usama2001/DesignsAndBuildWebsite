namespace DesignsAndBuild.Repository.Data.Configurations;

public class DesignsAndBuildContext : DbContext
{

    public DesignsAndBuildContext(DbContextOptions<DesignsAndBuildContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


    public DbSet<CustomerMessageDetails> CustomerMessages { get; set; }
    public DbSet<OurProjectImages> ProjectImags { get; set; }
    public DbSet<OurProject> Projects { get; set; }
}