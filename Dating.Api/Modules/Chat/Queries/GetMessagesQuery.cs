using Dating.Api.CqrsUtils;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.HttpErrors;
using Dating.Shared.Models.Chat;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Chat.Queries;

public sealed record GetMessagesQuery(Guid ChatId) : IHttpRequest;

public sealed class GetMessagesQueryHandler : IHttpRequestHandler<GetMessagesQuery>
{
    private readonly IUserContext _userContext;
    private readonly IDatingDbContext _context;

    public GetMessagesQueryHandler(IUserContext userContext, IDatingDbContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<IResult> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }

        var messages = await _context.Messages.Where(x => x.ChatFk == request.ChatId)
            .Select(x => new MessageVm
            {
                MessageId = x.Id,
                UserId = x.UserFk,
                MessageDate = x.MessageDate,
                Text = x.Text
            })
            .OrderByDescending(x => x.MessageDate)
            .ToListAsync(cancellationToken);
        
        return Results.Ok(messages);
    }
}