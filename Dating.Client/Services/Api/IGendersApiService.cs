using Dating.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Api
{
    internal interface IGendersApiService
    {
        public Task<List<GenderVm>> GetGendersAsync(CancellationToken cancellationToken = default);
    }
}
