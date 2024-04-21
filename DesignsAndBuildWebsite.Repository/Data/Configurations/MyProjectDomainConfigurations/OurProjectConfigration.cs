namespace DesignsAndBuild.Repository.Data.Configurations.MyProjectDomainConfigurations;


public class OurProjectConfigration : IEntityTypeConfiguration<OurProject>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;

    public void Configure(EntityTypeBuilder<OurProject> builder)
    {
        builder.ToTable("OurProjects");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(shortMaxLength);
        builder.Property(x => x.ArabicTitle)
            .IsRequired()
            .HasMaxLength(shortMaxLength);

        builder.Property(x => x.ClientName)
        .HasMaxLength(shortMaxLength);
        builder.Property(x => x.ArabicClientName)
            .HasMaxLength(shortMaxLength);

        builder.Property(x => x.Description)
        .IsRequired()
        .HasMaxLength(longMaxLength);
        builder.Property(x => x.ArabicDescription)
            .IsRequired()
            .HasMaxLength(longMaxLength);


        builder.HasMany(x => x.Images)
            .WithOne()
            .HasForeignKey(c => c.OurProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
