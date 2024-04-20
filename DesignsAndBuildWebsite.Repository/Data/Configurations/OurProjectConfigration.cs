using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesignsAndBuild.Repository.Data.Configurations;

public class OurProjectConfigration : IEntityTypeConfiguration<OurProject>
{
    public void Configure(EntityTypeBuilder<OurProject> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.ToTable("ClientProjects");

        builder.Property(x => x.Title)
            .IsRequired(true)
            .HasColumnType("nvarchar")
            .HasMaxLength(50);

        builder.Property(e=>e.ArabicTitle)
             .IsRequired(true)
            .HasColumnType("nvarchar")
            .HasMaxLength(50);

        builder.Property(x => x.ClientName)
           .IsRequired(true)
           .HasColumnType("nvarchar")
           .HasMaxLength(50);

        builder.Property(x => x.ArabicClientName)
          .IsRequired(true)
          .HasColumnType("nvarchar")
          .HasMaxLength(50);

        builder.Property(x => x.Description)
           .IsRequired(true)
           .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ArabicDescription)
           .IsRequired(true)
           .HasColumnType("nvarchar(max)");


        builder.Property(e=>e.Duration)
            .HasColumnType("nvarchar")
           .HasMaxLength(50);

        builder.Property(e => e.ArabicDuration)
           .HasColumnType("nvarchar")
          .HasMaxLength(50);

        builder.Property(x => x.VideoUrl)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");

        builder.HasMany(x => x.Imags)
            .WithOne()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
