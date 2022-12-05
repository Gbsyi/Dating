using Dating.Client.Services.Api;
using Dating.Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private IProfileApiService _profileApi;
        private readonly IServiceProvider _serviceProvider;
        private CreateProfileWindow? _createProfileWindow;
        private readonly HomeWindowViewModel _viewModel;

        public HomeWindow(IProfileApiService profileApi, IServiceProvider serviceProvider)
        {
            _profileApi = profileApi;
            _serviceProvider = serviceProvider;
            //_viewModel = serviceProvider.GetRequiredService<HomeWindowViewModel>();
            //DataContext = _viewModel;
            InitializeComponent();
            var dc = DataContext as HomeWindowViewModel;
            dc.InitDeps(serviceProvider.GetRequiredService<IPairService>(), serviceProvider.GetRequiredService<IPictureService>());
            _viewModel = dc;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var profile = await _profileApi.GetProfileAsync();
            if (profile is null)
            {
                _createProfileWindow = _serviceProvider.GetRequiredService<CreateProfileWindow>();
                var dc = _createProfileWindow.DataContext as CreateProfileViewModel;
                dc!.OnProfileCreatedEvent += LoadNextProfile;
                _createProfileWindow.Show();
            }
            else
            {
                await LoadNextProfile();
            }
        }

        private async Task LoadNextProfile()
        {
            _createProfileWindow?.Close();
            await _viewModel.LoadNextPair();
        }
    }
}
