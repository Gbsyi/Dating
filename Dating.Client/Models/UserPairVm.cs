using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Client.Models
{
    public sealed record UserPairVm
    {
        public Uri? PicturePath { get; init; }
        public required string Name { get; init; }
        public required Guid UserId { get; init; }
        public required Guid ChatId { get; init; }
    }
}
