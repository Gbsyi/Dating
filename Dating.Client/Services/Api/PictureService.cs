using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Api
{
    internal sealed class PictureService : IPictureService
    {

        public async Task<Stream> GetPictureAsync(Guid pictureId, CancellationToken cancellationToken = default)
        {
            using var http = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{AppConstants.BaseUrl}/pictures/{pictureId}");

            var response = await http.SendAsync(request, cancellationToken);
            
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
