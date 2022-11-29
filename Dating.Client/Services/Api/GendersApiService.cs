using Dating.Client.Services.Auth;
using Dating.Shared.Models;
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
    internal class GendersApiService : IGendersApiService
    {
        private readonly IAuthService _authService;

        public GendersApiService(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<List<GenderVm>> GetGendersAsync(CancellationToken cancellationToken = default)
        {
            var userId = _authService.GetUserId();
            using var http = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{AppConstants.BaseUrl}/gender/list");
            request.Headers.Add("IdAuth", userId.ToString());
            var response = await http.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<List<GenderVm>>();

            return result ?? new List<GenderVm>();
        }
    }
}
