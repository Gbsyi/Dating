using Dating.Client.Services.Auth;
using Dating.Shared.Models.Pair;
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
    internal class PairService : IPairService
    {
        private IAuthService _authService;

        public PairService(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<NextPairVm?> GetNextPairAsync(CancellationToken cancellationToken)
        {
            var userId = _authService.GetUserId();
            using var http = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{AppConstants.BaseUrl}/pair/next");
            request.Headers.Add("IdAuth", userId.ToString());
            var response = await http.SendAsync(request, cancellationToken);
            try
            {
                var result = await response.Content.ReadFromJsonAsync<NextPairVm?>();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
