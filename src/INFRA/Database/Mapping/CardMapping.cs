using DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INFRA.Database.Mapping
{
    public class CardMapping : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.Number).IsRequired();
            builder.Property(c => c.CVV).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt).IsRequired();

            builder.HasOne(c => c.Accounts)
                .WithMany(a => a.Cards)
                .HasForeignKey(c => c.AccountsId)
                .OnDelete(DeleteBehavior.Cascade); 

             
        }
    }
}
