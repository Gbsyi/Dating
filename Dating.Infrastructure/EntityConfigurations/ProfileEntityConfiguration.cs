using Dating.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dating.Infrastructure.EntityConfigurations;

internal sealed class ProfileEntityConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Age).HasComment("Возраст пользователя");
        builder.Property(x => x.Name).HasComment("Имя пользователя");
        builder.Property(x => x.Description).HasComment("Описание пользователя");
        builder.Property(x => x.GenderFk).HasComment("Пол пользователя");
        builder.HasOne<Gender>().WithMany().HasForeignKey(x => x.GenderFk);
        builder.HasOne<User>().WithOne().HasForeignKey<Profile>(x => x.Id);

    }
}