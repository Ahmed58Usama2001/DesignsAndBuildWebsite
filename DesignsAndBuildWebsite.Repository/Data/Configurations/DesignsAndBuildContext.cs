namespace DesignsAndBuild.Repository.Data.Configurations;

public class DesignsAndBuildContext : DbContext
{

    public DesignsAndBuildContext(DbContextOptions<DesignsAndBuildContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


    public DbSet<UserMessage> UserMessages { get; set; }
    public DbSet<OurProjectImages> ProjectImages { get; set; }
    public DbSet<OurProject> OurProjects { get; set; }
}