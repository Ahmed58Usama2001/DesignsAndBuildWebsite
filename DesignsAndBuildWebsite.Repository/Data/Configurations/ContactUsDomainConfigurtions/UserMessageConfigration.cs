namespace DesignsAndBuild.Repository.Data.Configurations.ContactUsDomainConfigurtions;

public class UserMessageConfigration : IEntityTypeConfiguration<UserMessage>
{
        const int shortMaxLength = 255;
        const int longMaxLength = 450;

    public void Configure(EntityTypeBuilder<UserMessage> builder)
    {

            builder.ToTable("UserMessages");

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(shortMaxLength);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(shortMaxLength);

            builder.Property(x => x.Email)
            .HasMaxLength(shortMaxLength);

            builder.Property(x => x.Message)
                .HasMaxLength(longMaxLength);

            builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(shortMaxLength);      
    }
}
