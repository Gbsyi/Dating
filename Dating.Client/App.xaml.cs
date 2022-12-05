using Dating.Client.Services.Api;
using Dating.Client.Services.Auth;
using Dating.Client.Services.Config;
using Dating.Client.Services.Store;
using Dating.Client.ViewModels;
using Dating.Client.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dating.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IConfigService, ConfigService>();
            services.AddTransient<IProfileApiService, ProfileApiService>();
            services.AddTransient<IGendersApiService, GendersApiService>();
            services.AddTransient<IPairService, PairService>();
            services.AddTransient<IPictureService, PictureService>();
            services.AddSingleton<ProfileStore>();

            // Pages
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<HomeWindow>();
            services.AddSingleton<CreateProfileWindow>();
            
            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<HomeWindowViewModel>();
            services.AddTransient<CreateProfileViewModel>();
        }
    }
}
