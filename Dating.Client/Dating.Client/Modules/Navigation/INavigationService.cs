using Dating.Client.Modules.Navigation.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Client.Modules.Navigation
{
    internal interface INavigationService
    {
        public void NavigateTo(List<string> Routes);
    }
}
