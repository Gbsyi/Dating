using Dating.Api.CqrsUtils;
using Dating.Domain.Enums;
using Dating.Domain.Models;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.HttpErrors;
using Dating.Shared.Models.Pair;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Pair.Commands;

public sealed record LikePairCommand(LikePairVm Vm) : IHttpRequest;

public class LikePairCommandHandler : IHttpRequestHandler<LikePairCommand>
{
    private readonly IDatingDbContext _context;
    private readonly IUserContext _userContext;
    
    public LikePairCommandHandler(IDatingDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<IResult> Handle(LikePairCommand request, CancellationToken cancellationToken)
    {
        var vm = request.Vm;
        var userId = _userContext.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }

        var profile = await _context.Profiles.Where(x => x.Id == userId).FirstOrDefaultAsync(cancellationToken);
        if (profile is null)
        {
            return Results.Unauthorized();
        }
        var likedProfile = await _context.Profiles.Where(x => x.Id == vm.LikedProfileId)
            .FirstOrDefaultAsync(cancellationToken);
        if (likedProfile is null)
        {
            return Results.BadRequest(new BadRequestVm
            {
                ErrorMessage = "Профиль не найден"
            });
        }

        var dbLike = await _context.Likes.Where(x => x.PairFk == userId && x.UserFk == likedProfile.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        // Создаём лайк
        var like = new Like
        {
            UserFk = profile.Id,
            PairFk = likedProfile.Id,
            PairStatus = PairStatusEnum.Liked
        };
        _context.Likes.Add(like);

        // Если найден существующий лайк
        if (dbLike is not null)
        {
            // Если лайк не взаимный - сохраняем лайки. Пару не создаём
            if (dbLike.PairStatus == PairStatusEnum.Rejected)
            {
                like.PairStatus = PairStatusEnum.Rejected;
                _context.Likes.Add(like);
                await _context.SaveChangesAsync(cancellationToken);
                return Results.Ok(new LikePairResultVm
                {
                    IsMutual = true
                });
            }

            // Если лайк взаимный - создаём пару, лайки удаляем
            if (dbLike.PairStatus == PairStatusEnum.Liked)
            {
                var chat = new Domain.Models.Chat
                {
                    Id = Guid.NewGuid(),
                    IsActive = true
                };

                var pair = new Domain.Models.Pair
                {
                    UserFk = profile.Id,
                    MatchedUserFk = likedProfile.Id,
                    ChatFk = chat.Id
                };
                
                var pair2 = new Domain.Models.Pair
                {
                    UserFk = likedProfile.Id, 
                    MatchedUserFk = profile.Id,
                    ChatFk = chat.Id
                };

                _context.Pairs.Add(pair);
                _context.Pairs.Add(pair2);
                _context.Chats.Add(chat);
                await _context.SaveChangesAsync(cancellationToken);
                return Results.Ok(new LikePairResultVm
                {
                    IsMutual = true
                });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Results.Ok(new LikePairResultVm
        {
            IsMutual = false
        });
    }
}