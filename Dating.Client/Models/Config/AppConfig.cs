using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Client.Models.Config
{
    public sealed record AppConfig
    {
        public Guid? UserId { get; init; }
    }
}
