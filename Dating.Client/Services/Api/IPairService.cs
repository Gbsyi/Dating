using Dating.Shared.Models.Pair;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Api
{
    public interface IPairService
    {
        public Task<NextPairVm?> GetNextPairAsync(CancellationToken cancellationToken = default);
        public Task<IList<PairVm>> GetUserPairsAsync(CancellationToken cancellationToken = default);
        public Task<LikePairResultVm> LikeProfileAsync(LikePairVm vm, CancellationToken cancellationToken = default);
        public Task<bool> DisikeProfileAsync(LikePairVm vm, CancellationToken cancellationToken = default);
    }
}
