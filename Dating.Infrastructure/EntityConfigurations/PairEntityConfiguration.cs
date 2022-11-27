using Dating.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dating.Infrastructure.EntityConfigurations;

public class PairEntityConfiguration : IEntityTypeConfiguration<Pair>
{
    public void Configure(EntityTypeBuilder<Pair> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserFk);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.MatchedUserFk);
        builder.HasOne<Chat>().WithMany().HasForeignKey(x => x.ChatFk);
    }
}