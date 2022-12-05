using Dating.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dating.Infrastructure;

public interface IDatingDbContext
{
    public DbSet<User> Users { get; init; }
    public DbSet<Profile> Profiles { get; init; }
    public DbSet<Pair> Pairs { get; init; }
    public DbSet<Message> Messages { get; init; }
    public DbSet<Like> Likes { get; init; }
    public DbSet<Chat> Chats { get; init; }
    public DbSet<Gender> Genders { get; init; }
    public DbSet<UserOrientation> UserOrientations { get; init; }
    public DbSet<Picture> Pictures { get; init; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
