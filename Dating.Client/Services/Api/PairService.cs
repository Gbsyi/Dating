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

        public async Task<bool> DisikeProfileAsync(LikePairVm vm, CancellationToken cancellationToken = default)
        {
            var userId = _authService.GetUserId();
            using var http = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{AppConstants.BaseUrl}/pair/dislike");
            request.Headers.Add("IdAuth", userId.ToString());
            request.Content = JsonContent.Create(vm);
            var response = await http.SendAsync(request, cancellationToken);
            return response.IsSuccessStatusCode;
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
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<PairVm>> GetUserPairsAsync(CancellationToken cancellationToken = default)
        {
            var userId = _authService.GetUserId();
            using var http = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, $"{AppConstants.BaseUrl}/pair/pairs");
            request.Headers.Add("IdAuth", userId.ToString());
            var response = await http.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<IList<PairVm>>();
            return result;
        }

        public async Task<LikePairResultVm> LikeProfileAsync(LikePairVm vm, CancellationToken cancellationToken = default)
        {
            var userId = _authService.GetUserId();
            using var http = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{AppConstants.BaseUrl}/pair/like");
            request.Headers.Add("IdAuth", userId.ToString());
            request.Content = JsonContent.Create(vm);
            var response = await http.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<LikePairResultVm>();
            return result;
        }
    }
}
