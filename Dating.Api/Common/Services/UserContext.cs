using Dating.Infrastructure.Services;

namespace Dating.Api.Common.Services;

public sealed class UserContext : IUserContext
{
    private readonly HttpContext _httpContext;
    
    public Guid? UserId => TryGetUserId();

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext ?? throw new ApplicationException("Контекст отсутствует");
    }

    private Guid? TryGetUserId()
    {
        var authHeader = _httpContext.Request.Headers["IdAuth"];
        var idString = authHeader.FirstOrDefault();

        if (Guid.TryParse(idString, out var guid))
        {
            return guid;
        }

        return null;
    }

}