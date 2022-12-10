using Dating.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dating.Infrastructure.EntityConfigurations;

public class LikeEntityConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserFk);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.PairFk);
    }
}