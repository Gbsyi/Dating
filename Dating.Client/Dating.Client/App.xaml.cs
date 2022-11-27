using Dating.Client.Modules.Navigation;
using Dating.Client.Pages.Home;
using Dating.Client.Services;
using Dating.Client.Services.Abstractions;
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

        private void ConfigureServices(IServiceCollection services)
        {
            // Регистрируем сервисы
            services.AddSingleton<IAuthService, AuthService>();

            // Регистрируем основное окно
            services.AddSingleton<MainWindow>();
            services.AddSingleton<NavigationService>();

            // Регистрируем страницы
            services.AddTransient<Home>();
            services.AddTransient<PairsPage>();
            services.AddTransient<ProfilePage>();

            // Регистрируем вью-модели
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}