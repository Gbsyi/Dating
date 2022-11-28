using CommunityToolkit.Mvvm.Input;
using Dating.Client.Services.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dating.Client.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        private string _username = "";
        public string Username 
        { 
            get => _username; 
            set 
            { 
                _username = value;
                NotifyChanged();
            } 
        }
        private string _password = "";
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyChanged();
            }
        }
        private string? _error = null;
        public string? Error
        {
            get => _error;
            set
            {
                _error = value;
                NotifyChanged();
            }
        }
        public IAsyncRelayCommand LoginCommand { get; private set; }

        public event Action? LoginCompletedEvent;

        public LoginViewModel(IAuthService authService)
        {
            LoginCommand = new AsyncRelayCommand(
                async (CancellationToken ct) =>
            {
                await authService.TryLoginAsync(Username, Password, ct);
                LoginCompletedEvent?.Invoke();
            });
        }

    }
}
