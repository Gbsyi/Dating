using Dating.Client.Models.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Dating.Client.Services.Config
{
    internal class ConfigService : IConfigService, IDisposable
    {
        private bool _disposed = false;
        public AppConfig Config { get; private set; } = null!;

        public AppConfig LoadConfig()
        {
            if (!File.Exists("app_data.json"))
            {
                Config = new AppConfig();
                return Config;
            }

            using var sr = new StreamReader("app_data.json");

            var configString = sr.ReadToEnd();
            var config = JsonSerializer.Deserialize<AppConfig>(configString);
            if (config == null)
            {
                Config = new AppConfig();
                return Config;
            }

            return config;
        }

        public void UpdateConfig(AppConfig config)
        {
            Config = config;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                using var sw = new StreamWriter("app_data.json");
                sw.Write(JsonSerializer.Serialize(Config));
            }
            GC.SuppressFinalize(this);
        }
    }
}
