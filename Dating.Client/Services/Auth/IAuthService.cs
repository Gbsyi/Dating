using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Auth
{
    public interface IAuthService
    {
        Guid GetUserId();
        Task<bool> TryAuthenticateAsync(Guid? UserId, CancellationToken cancellationToken);
        Task<bool> TryLoginAsync(string login, string password, CancellationToken cancellationToken);
    }
}
