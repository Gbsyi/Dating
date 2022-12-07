using Dating.Api.CqrsUtils;
using Dating.Api.Modules.Chat.Commands;
using Dating.Api.Modules.Chat.Queries;

namespace Dating.Api.Modules.Chat;

public static class ChatApiConfig
{
    public static void AddChatEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("chat").WithTags("Chat");

        group.MediateGet<GetMessagesQuery>("/messages");
        group.MediatePost<SendMessageCommand>("/send-message");
    }
}