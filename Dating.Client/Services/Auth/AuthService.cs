using Dating.Client.Services.Config;
using Dating.Shared.Models.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Auth
{
    internal class AuthService : IAuthService
    {
        private readonly IConfigService _configService;

        public AuthService(IConfigService configService)
        {
            _configService = configService;
        }

        private Guid _userId;

        public Guid GetUserId()
        {
            return _userId;
        }

        public Task<bool> TryAuthenticateAsync(Guid? userId, CancellationToken cancellationToken)
        {
            if (TryLoadAuthFromConfig())
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        private bool TryLoadAuthFromConfig()
        {
            var config = _configService.LoadConfig();   
            if (config.UserId is null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> TryLoginAsync(string login, string password, CancellationToken cancellationToken)
        {
            using var http = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"{AppConstants.BaseUrl}/account/login"));
            request.Content = JsonContent.Create(new LoginVm
            {
                Username = login,
                Password = password,
            });

            var response = await http.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<LoginResultVm>();
            if (result is null)
            {
                return false;
            }


            _userId = result.UserId;
            _configService.UpdateConfig(_configService.Config with
            {
                UserId = result.UserId,
            });
            return true;
        }
    }
}
