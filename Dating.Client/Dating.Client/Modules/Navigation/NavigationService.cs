using Dating.Client.Modules.Navigation.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Client.Services
{
    internal sealed class NavigationService
    {
        private Endpoint _rootEndpoint;

        private string[] _currentRoute;

        public NavigationService()
        {
            ConfigureNavigation();
        }

        public string NavigateTo(string[] routes)
        {
            throw new NotImplementedException();
        }

        public void ListenEndpoint(string[] routes, Action onChange)
        {

        }

        #region Configuration
        private void ConfigureNavigation()
        {
            _rootEndpoint = new Endpoint
            {
                
            }
        }
        #endregion
    }
}
