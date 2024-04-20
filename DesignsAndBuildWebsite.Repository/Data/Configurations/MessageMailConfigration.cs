namespace DesignsAndBuild.Repository.Data.Configurations;

public class MessageMailConfigration : IEntityTypeConfiguration<CustomerMessageDetails>
{

    public void Configure(EntityTypeBuilder<CustomerMessageDetails> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(e => e.FirstName).IsRequired(true)
            .HasMaxLength(50).HasColumnType("nvarchar");

        builder.Property(e => e.LastName).IsRequired(true)
          .HasMaxLength(50).HasColumnType("nvarchar");

        builder.Property(e => e.Email).IsRequired(true)
            .HasColumnType("nvarchar");

        builder.Property(e => e.Phone).IsRequired(true);

        builder.Property(e => e.Message).IsRequired(true);

        builder.Property(e => e.SeenByWho).IsRequired(false);

        builder.Property(e => e.SendMessageDate).IsRequired(false);

        builder.Property(e => e.DateMessageSeenAt).IsRequired(false);
    }
}
