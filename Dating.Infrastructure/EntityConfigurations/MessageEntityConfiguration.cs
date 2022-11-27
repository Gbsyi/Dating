using Dating.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dating.Infrastructure.EntityConfigurations;

public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<Chat>().WithMany().HasForeignKey(x => x.ChatFk);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserFk);
    }
}