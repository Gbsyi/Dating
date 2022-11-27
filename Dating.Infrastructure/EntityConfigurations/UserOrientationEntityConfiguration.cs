using Dating.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dating.Infrastructure.EntityConfigurations;

public class UserOrientationEntityConfiguration : IEntityTypeConfiguration<UserOrientation>
{
    public void Configure(EntityTypeBuilder<UserOrientation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<Gender>().WithMany().HasForeignKey(x => x.GenderFk);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserFk);
    }
}