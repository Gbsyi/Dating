using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Api
{
    public interface IPictureService
    {
        public Task<Stream> GetPictureAsync(Guid pictureId, CancellationToken cancellationToken = default);
    }
}
