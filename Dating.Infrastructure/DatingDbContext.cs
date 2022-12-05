using Dating.Domain.Models;
using Dating.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Dating.Infrastructure;

public class DatingDbContext : DbContext, IDatingDbContext
{
    public required DbSet<User> Users { get; init; }
    public required DbSet<Profile> Profiles { get; init; }
    public required DbSet<Pair> Pairs { get; init; }
    public required DbSet<Message> Messages { get; init; }
    public required DbSet<Like> Likes { get; init; }
    public required DbSet<Chat> Chats { get; init; }
    public required DbSet<Gender> Genders { get; init; }
    public required DbSet<UserOrientation> UserOrientations { get; init; }
    public required DbSet<Picture> Pictures { get; init; }

    public DatingDbContext(DbContextOptions<DatingDbContext> options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserOrientationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PairEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new LikeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new GenderEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ChatEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}