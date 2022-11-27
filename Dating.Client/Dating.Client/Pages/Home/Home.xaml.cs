using System;
using System.Windows;
using System.Windows.Controls;

namespace Dating.Client.Pages.Home;

public partial class Home : Page
{
    public Home()
    {
        InitializeComponent();
    }
    
    private void OnLoading(object sender, RoutedEventArgs e)
    {
        Nav.Navigate(new PairsPage());
    }

    private void PairsClick(object sender, RoutedEventArgs e)
    {
        Nav.Navigate(new PairsPage());
    }
        
    private void ProfileClick(object sender, RoutedEventArgs e)
    {
        Nav.Navigate(new ProfilePage());
    }
}