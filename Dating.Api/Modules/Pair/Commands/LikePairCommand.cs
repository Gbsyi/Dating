﻿using Dating.Api.CqrsUtils;
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

        var dbLike = await _context.Likes.Where(x => x.PairFk == likedProfile.Id || x.UserFk == likedProfile.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        // Создаём лайк
        var like = new Like
        {
            PairFk = likedProfile.Id,
            UserFk = profile.Id,
            PairStatus = PairStatusEnum.Liked
        };
        

        // Если найден существующий лайк
        if (dbLike is not null)
        {
            // Если лайк не взаимный - сохраняем лайки. Пару не создаём
            if (dbLike.PairStatus == PairStatusEnum.Rejected)
            {
                like.PairStatus = PairStatusEnum.Rejected;
                await _context.SaveChangesAsync(cancellationToken);
                return Results.Ok();
            }

            // Если лайк взаимный - создаём пару, лайки удаляем
            if (dbLike.PairStatus == PairStatusEnum.Liked)
            {
                var chat = new Chat
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

                _context.Pairs.Add(pair);
                _context.Chats.Add(chat);
                _context.Likes.Remove(dbLike);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Results.Ok();
    }
}