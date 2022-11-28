using Dating.Client.Services.Auth;
using Dating.Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dating.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAuthService _authService;
        private IServiceProvider _serviceProvider;

        private CancellationTokenSource _lifetimeCts;
        public LoginWindow(IAuthService authService, IServiceProvider serviceProvider)
        {
            _authService = authService;
            _lifetimeCts = new CancellationTokenSource();
            _serviceProvider = serviceProvider;
            
            var vm = serviceProvider.GetRequiredService<LoginViewModel>();
            vm.LoginCompletedEvent += OnUserAuthenticated;
            
            InitializeComponent();
            DataContext = vm;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _lifetimeCts.Cancel();
            base.OnClosing(e);
        }

        private async Task AuthenticateAsync()
        {
            var isAuthenticated = await _authService.TryAuthenticateAsync(null, _lifetimeCts.Token);
            if (isAuthenticated)
            {
                OnUserAuthenticated();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await AuthenticateAsync();
        }

        private void OnUserAuthenticated()
        {
            var homeWindow = _serviceProvider.GetRequiredService<HomeWindow>();
            homeWindow.Show();
            Hide();
        }
    }
}
