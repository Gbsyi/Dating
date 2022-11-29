using Dating.Shared.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Api
{
    public interface IProfileApiService
    {
        public Task<ProfileVm?> GetProfileAsync(CancellationToken cancellationToken = default);
        public Task<CreateProfileResultVm> CreateProfileAsync(CreateProfileVm profile, CancellationToken cancellationToken = default);
    }
}
