namespace DesignsAndBuild.Repository.Data.Configurations;

public class MessageMailConfigration : IEntityTypeConfiguration<CustomerMessageDetails>
{

    public void Configure(EntityTypeBuilder<CustomerMessageDetails> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

      
    }
}
