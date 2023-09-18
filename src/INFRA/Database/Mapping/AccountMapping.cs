using DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INFRA.Database.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Accounts>
    {
        public void Configure(EntityTypeBuilder<Accounts> builder)
        {
            builder.ToTable("Accounts") 
                    .HasKey(a => a.Id); 

            builder.Property(a => a.Branch)
                .IsRequired() 
                .HasMaxLength(255); 

            builder.Property(a => a.Account)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.UpdatedAt)
                .IsRequired();

            builder.Property(a => a.Balance)
                .IsRequired();

            builder.Property(a => a.PeopleId)
                .IsRequired();

            builder.HasOne(a => a.People)
                .WithMany(p => p.Accounts)
                .HasForeignKey(a => a.PeopleId);


        }
    }
}
