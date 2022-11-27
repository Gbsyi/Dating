using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Client.Modules.Navigation.Routes
{
    internal abstract class Endpoint
    {
        public Endpoint? Parent { get; }

        private event Action<string> _navigation;

        public Endpoint(Endpoint? parent)
        {
            Parent = parent;    
        }

        public void ListenEndpoint(Action<string> handler)
        {
            _navigation += handler;
        }

        public void Navigate(string route)
        {
            _navigation?.Invoke(route);
        }
    }
}
