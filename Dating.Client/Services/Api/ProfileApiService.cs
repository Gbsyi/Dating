using Dating.Client.Services.Auth;
using Dating.Shared.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Api
{
    internal class ProfileApiService : IProfileApiService
    {
        private IAuthService _authService;

        public ProfileApiService(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<CreateProfileResultVm> CreateProfileAsync(CreateProfileVm profile, CancellationToken cancellationToken = default)
        {
            // TODO: Создание профиля
            throw new NotImplementedException();
        }

        public async Task<ProfileVm?> GetProfileAsync(CancellationToken cancellationToken = default)
        {
            var userId = _authService.GetUserId();
            using var http = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{AppConstants.BaseUrl}/profile");
            request.Headers.Add("IdAuth", userId.ToString());
            var response = await http.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<GetProfileResult>();
            if (result is null)
            {
                throw new ApplicationException("Пустой ответ");
            }

            if (result.IsCreated)
            {
                return result?.Profile;
            }

            return null;
        }
    }
}
