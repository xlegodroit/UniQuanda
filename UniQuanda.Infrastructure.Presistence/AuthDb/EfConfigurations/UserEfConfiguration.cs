using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.EfConfigurations
{
    public class UserEfConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Nickname).HasMaxLength(30).IsRequired();
            builder.HasIndex(u => u.Nickname).IsUnique();

            builder.Property(u => u.HashedPassword).IsRequired();

            builder.Property(u => u.RefreshToken).IsRequired(false);
            builder.Property(u => u.RefreshTokenExp).IsRequired(false);

            builder
                .HasOne(u => u.IdTempUserNavigation)
                .WithOne(tu => tu.IdUserNavigation)
                .HasForeignKey<TempUser>(tu => tu.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(u => u.Emails)
                .WithOne(ue => ue.IdUserNavigation)
                .HasForeignKey(ue => ue.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}