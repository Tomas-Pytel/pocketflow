using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Data.Configurations
{
    public class SavingConfiguration : IEntityTypeConfiguration<Saving>
    {
        public void Configure(EntityTypeBuilder<Saving> builder)
        {
            builder.Property(s => s.Amount).HasPrecision(18, 2).IsRequired();
            builder.Property(s => s.Limit).HasPrecision(18, 2);
            builder.Property(s => s.Name).HasMaxLength(50).IsRequired();

            builder.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => new { s.UserId, s.Name }).IsUnique();
        }
    }
}
