using Dating.Api.CqrsUtils;
using Dating.Domain.Models;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.HttpErrors;
using Dating.Shared.Models.Chat;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Chat.Commands;

public sealed record SendMessageCommand(MessageSendVm Vm) : IHttpRequest;

public class SendMessageCommandHandler : IHttpRequestHandler<SendMessageCommand>
{
    private readonly IUserContext _userContext;
    private readonly IDatingDbContext _context;

    public SendMessageCommandHandler(IUserContext userContext, IDatingDbContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<IResult> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var vm = request.Vm;
        var userId = _userContext.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }

        var chat = await _context.Chats.Where(x => x.Id == vm.ChatId).FirstOrDefaultAsync(cancellationToken);
        if (chat is null)
        {
            return Results.BadRequest(new BadRequestVm
            {
                ErrorMessage = "Чат не найден"
            });
        }

        var message = new Message
        {
            ChatFk = chat.Id,
            MessageDate = DateTime.UtcNow,
            UserFk = userId.Value,
            Text = vm.Text
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Results.Ok();
    }
}