using Dating.Client.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Config
{
    public interface IConfigService
    {
        AppConfig Config { get; }
        
        AppConfig LoadConfig();
        void UpdateConfig(AppConfig config);
    }
}
