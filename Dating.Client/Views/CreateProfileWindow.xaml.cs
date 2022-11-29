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
    /// Логика взаимодействия для CreateProfileWindow.xaml
    /// </summary>
    public partial class CreateProfileWindow : Window
    {
        public CreateProfileWindow(IServiceProvider serviceProvider)
        {
            var vm = serviceProvider.GetRequiredService<CreateProfileViewModel>();
            DataContext = vm;
            InitializeComponent();
        }
    }
}
